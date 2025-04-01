using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.Class;
using System;
using System.Diagnostics.CodeAnalysis;
using API.ClassDTO;
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
                TotalRounds = request.TotalRounds,
                DifficultyLevel = request.DifficultyLevel,
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
            error = "Invalid input. Please provide a valid shape and session ID."
        });
    }

    // Find the game session based on GameSessionId
    var gameSession = _context.GameSession.FirstOrDefault(gs => gs.Id == playerShapeRequest.GameSessionId);
    if (gameSession == null)
    {
        return BadRequest(new
        {
            error = "Game session could not be found."
        });
    }

    var player = _context.Players.FirstOrDefault(p => p.Id == gameSession.PlayerId);
    if (player == null)
    {
        return BadRequest(new
        {
            error = "Are you sure you are a player? I didn't find you in my database."
        });
    }

    // Choose player shape by number
    player.ChooseShape(CreateShape(playerShapeRequest.PlayerShape));

    enShapeType computerShape;

    // Determine computer's shape based on difficulty level
    if (gameSession.DifficultyLevel == enDifficultyLevel.Medium)
    {
        computerShape = (enShapeType)new Random().Next(1, 4); 
    }
    else if (gameSession.DifficultyLevel == enDifficultyLevel.Hard)
    {
        // Implement AI logic for hard difficulty
        computerShape = GetComputerShapeForHardDifficulty(player.Id);
    }
    else
    {
        // Default to random if difficulty is unknown or easy
        computerShape = (enShapeType)new Random().Next(1, 4);
    }

    var computer = new Player("Computer");
    computer.ChooseShape(CreateShape(computerShape));

    var result = DetermineWinner(player, computer);
    
    
    // Check number of rounds
    var gameHistoryCount = _context.GameHistories.Count(gh => gh.GameSessionId == playerShapeRequest.GameSessionId);
    if (gameHistoryCount >= gameSession.TotalRounds)
    {
        return BadRequest(new
        {
            error = "You have reached the maximum number of rounds for this session."
        });
    }

    // Save in database
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

private enShapeType GetComputerShapeForHardDifficulty(int playerId)
{
    var lastGames = _context.GameHistories
        .Where(gh => gh.PlayerId == playerId)
        .OrderByDescending(gh => gh.PlayedAt)
        .Take(10)
        .ToList();

    if (lastGames.Count == 0)
    {
        return (enShapeType)new Random().Next(1, 4);
    }

    var shapeCounts = lastGames.GroupBy(gh => gh.PlayerShape)
        .Select(g => new { Shape = g.Key, Count = g.Count() })
        .OrderByDescending(g => g.Count)
        .ToList();

    var mostPlayedShape = shapeCounts.First().Shape;

    switch (mostPlayedShape)
    {
        case enShapeType.Rond:
            return enShapeType.Rectangle;  
        case enShapeType.Rectangle:
            return enShapeType.Triangle;  
        case enShapeType.Triangle:
            return enShapeType.Rond;  
        default:
            return (enShapeType)new Random().Next(1, 4); 
    }
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
                return $"{player.Name} wins with Triangle!";
            else if (player.GetEnumShape() == enShapeType.Rectangle && computer.GetEnumShape() == enShapeType.Rond)
                return $"{player.Name} wins with Rectangle!";
            else if (player.GetEnumShape() == enShapeType.Rond && computer.GetEnumShape() == enShapeType.Triangle)
                return $"{player.Name} wins with Rond!";
            else if (computer.GetEnumShape() == enShapeType.Triangle && player.GetEnumShape() == enShapeType.Rectangle)
                return "Computer wins with Triangle!";
            else if (computer.GetEnumShape() == enShapeType.Rectangle && player.GetEnumShape() == enShapeType.Rond)
                return "Computer wins with Rectangle!";
            else if (computer.GetEnumShape() == enShapeType.Rond && player.GetEnumShape() == enShapeType.Triangle)
                return "Computer wins with Rond!";
            else
                return "It's a tie!";
        }
        
        // get session histiory by id 
        [HttpGet("session/{sessionId}")]
        public IActionResult GetSessionById(int sessionId)
        {
            var gameHistories = _context.GameHistories
                .Where(h => h.GameSessionId == sessionId)
                .Join(_context.Players,
                    gh=>gh.PlayerId,
                    p => p.Id,
                    (gh,p)=> new GameSessionDTO()
                    {
                        Id = gh.Id,
                        GameSessionId = gh.GameSessionId,
                        PlayerName = p.Name,
                        PlayerShape = ((enShapeType)gh.PlayerShape).ToString(),
                        ComputerShape = ((enShapeType)gh.ComputerShape).ToString(),
                        Result = gh.Result,
                        PlayedAt = gh.PlayedAt
                    })
                .ToList();

            if (!gameHistories.Any())
            {
                return NotFound(new { message = "No game history found for this session." });
            }
            return Ok(gameHistories);
        }
        
        // get session result
        [HttpGet("session-result/{sessionId}")]
        public IActionResult GetSessionResult(int sessionId)
        {
            var gameHistories = _context.GameHistories
                .Where(h => h.GameSessionId == sessionId)
                .ToList();

            if (!gameHistories.Any())
            {
                return NotFound(new { message = "No game history found for this session." });
            }

            int playerWins = gameHistories.Count(h => h.Result.Contains("wins"));
            int computerWins = gameHistories.Count(h => h.Result.Contains("Computer wins"));

            return Ok(new { playerWins, computerWins });
        }

        // get history 
        [HttpGet("sessions")]
        public IActionResult GetAllSessionIds()
        {
            var sessionIds = _context.GameSession
                .OrderByDescending(s => s.Id)
                .Select(s => s.Id)
                .ToList();
    
            return Ok(sessionIds);
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
        
        public enDifficultyLevel DifficultyLevel { get; set; }
    }
    
    
    
    
}
