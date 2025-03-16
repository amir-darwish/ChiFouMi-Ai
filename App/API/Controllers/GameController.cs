using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.Class;
using System;
using App.Data;

namespace App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly AppDbContext _context;

        public GameController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("play")]
        public IActionResult PlayGame([FromBody] PlayerShapeRequest playerShapeRequest)
        {
            if (playerShapeRequest == null || string.IsNullOrWhiteSpace(playerShapeRequest.PlayerName) ||
                !Enum.IsDefined(typeof(enShapeType), playerShapeRequest.PlayerShape))
            {
                return BadRequest("Invalid input. Please provide a valid player name and shape.");
            }

            // search and create Player if not exist in database
            var player = _context.Players.FirstOrDefault(p => p.Name == playerShapeRequest.PlayerName);
            if (player == null)
            {
                player = new Player(playerShapeRequest.PlayerName);
                _context.Players.Add(player);
                _context.SaveChanges(); 
            }

           // chose player shape by number
            var playerId = player.Id;
            player.ChooseShape(CreateShape(playerShapeRequest.PlayerShape));

            // chose computer shape randomly 
            var computerShape = (enShapeType)new Random().Next(1, 4);
            var computer = new Player("Computer");
            computer.ChooseShape(CreateShape(computerShape));

            
            var result = DetermineWinner(player, computer);

           // save in database
            var gameHistory = new GameHistory
            {
                PlayerId = playerId,  
                PlayerShape = playerShapeRequest.PlayerShape,
                ComputerShape = computerShape,
                Result = result,
                PlayedAt = DateTime.UtcNow
            };

            _context.GameHistories.Add(gameHistory);
            _context.SaveChanges();

          // return le resulte
            return Ok(new
            {
                PlayerName = playerShapeRequest.PlayerName,
                PlayerShape = playerShapeRequest.PlayerShape,
                ComputerShape = computerShape,
                Result = result
            });
        }


        private Form CreateShape(enShapeType shapeType)
        {
            switch (shapeType)
            {
                case enShapeType.Triangle:
                    return new Triangle(5, 4, 3, "Green");
                case enShapeType.Rectangle:
                    return new Rectangle(5,3,"Red");
                case enShapeType.Rond:
                    return new Rond(5, "Blue");
                default:
                    throw new ArgumentException("Invalid shape type");
            }
        }


        private string DetermineWinner(Player player, Player computer)
        {
            if (player.GetEnumShape() == enShapeType.Triangle && computer.GetEnumShape() == enShapeType.Rectangle)
                return "Player wins with Triangle!";
            else if (player.GetEnumShape() == enShapeType.Rectangle && computer.GetEnumShape() == enShapeType.Rond)
                return "Player wins with Rectangle!";
            else if (player.GetEnumShape() == enShapeType.Rond && computer.GetEnumShape() == enShapeType.Triangle)
                return "Player wins with Rond!";
            else if (computer.GetEnumShape() == enShapeType.Triangle && player.GetEnumShape() == enShapeType.Rectangle)
                return "Computer wins with Triangle!";
            else if (computer.GetEnumShape() == enShapeType.Rectangle && player.GetEnumShape() == enShapeType.Rond)
                return "Computer wins with Rectangle!";
            else if (computer.GetEnumShape() == enShapeType.Rond && player.GetEnumShape() == enShapeType.Triangle)
                return "Computer wins with Rond!";
            else
                return "It's a tie!";
        }
        
        // get history 
        [HttpGet("history")]
        public IActionResult GetGameHistory()
        {
            var history = _context.GameHistories.ToList();
            return Ok(history);
        }

    }

    public class PlayerShapeRequest
    {
        public string PlayerName { get; set; }
        public enShapeType PlayerShape { get; set; }
    }
    
    
    
}
