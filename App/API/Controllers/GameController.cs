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
        [HttpPost("start-session")]
        public IActionResult StartSession([FromBody] GameSessionRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.PlayerName) || request.TotalRounds <= 0)
            {
                return BadRequest(new {error = "Invalid input. Please provide a valid player name and number of rounds."});
            }

            //Search for player and add it if not exist
            var player = _context.Players.FirstOrDefault(p => p.Name == request.PlayerName);
            if (player == null)
            {
                player = new Player(request.PlayerName);
                _context.Players.Add(player);
                _context.SaveChanges();
            }

            // create a session
            var gameSession = new GameSession()
            {
                PlayerId = player.Id,
                TotalRounds = request.TotalRounds
            };

            _context.GameSession.Add(gameSession);
            _context.SaveChanges();

            return Ok(new
            {
                Status = "Success",
                Message = "Game session started!", SessionId = gameSession.Id
            });
        }

[HttpPost("play")]
public IActionResult PlayGame([FromBody] PlayerShapeRequest playerShapeRequest)
{
    if (playerShapeRequest == null || !Enum.IsDefined(typeof(enShapeType), 
            playerShapeRequest.PlayerShape) || playerShapeRequest.GameSessionId == 0)
    {
        return BadRequest(new
        {
            erorr ="Invalid input. Please provide a valid shape and session ID."
        });
    }

    // Find the game session based on GameSessionId
    var gameSession = _context.GameSession.FirstOrDefault(gs => gs.Id == playerShapeRequest.GameSessionId);
    if (gameSession == null)
    {
        return BadRequest(new
        {
            error =  "Game session could not be found."
        });
    }

    var player = _context.Players.FirstOrDefault(p => p.Id == gameSession.PlayerId);
    if (player == null)
    {
        return BadRequest(new
        {
            error = "Are you sure you are a player ? I didn't find you in my datebase. "
        });
    }

    // chose player shape by number
    player.ChooseShape(CreateShape(playerShapeRequest.PlayerShape));

    // chose computer shape randomly
    var computerShape = (enShapeType)new Random().Next(1, 4);
    var computer = new Player("Computer");
    computer.ChooseShape(CreateShape(computerShape));

    var result = DetermineWinner(player, computer);

    // Check number of rounds
    var gameHistoryCount = _context.GameHistories.Count(gh => gh.GameSessionId == playerShapeRequest.GameSessionId);
    if (gameHistoryCount >= gameSession.TotalRounds)
    {
        return BadRequest("You have reached the maximum number of rounds for this session.");
    }

    // save in database
    var gameHistory = new GameHistory
    {
        GameSessionId = playerShapeRequest.GameSessionId,
        PlayerId = player.Id,
        PlayerShape = playerShapeRequest.PlayerShape,
        ComputerShape = computerShape,
        Result = result,
        PlayedAt = DateTime.UtcNow
    };

    _context.GameHistories.Add(gameHistory);
    _context.SaveChanges();

    return Ok(new
    {
        PlayerName = player.Name,
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
        public enShapeType PlayerShape { get; set; }
        public int GameSessionId { get; set; } 

    }
    public class GameSessionRequest
    {
        public string PlayerName { get; set; }
        public int TotalRounds { get; set; } 
    }
    
    
    
}
