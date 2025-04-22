using Microsoft.Extensions.Options;

namespace App.Class;
using App.Data;
using Microsoft.EntityFrameworkCore;

public class Game
{
    private Player _player1;
    private Player _computer;
    private AppDbContext _context;
    private ShapeManager _shapeManager;
    private string _playerName;
    private short _numberOfRounds;
    private short _difficulty;
    private GameSession _gameSession;
    private GameHistory _gameHistory;
    
    public Game()
    {
         _difficulty = 0;
        Console.WriteLine("Enter your name:");
         string _playerName = Console.ReadLine();
         Console.WriteLine("Enter the number of rounds you would like to play:");
         _numberOfRounds = short.Parse(Console.ReadLine());
         while (_difficulty > 3 || _difficulty <= 0)
         {
             Console.WriteLine("Enter the difficulty you would like to play:");
             Console.WriteLine("1- Easy \n2- Medium \n3- Hard:");
             _difficulty = short.Parse(Console.ReadLine());
         }
         
        _computer = new Player("Computer");
        _shapeManager = new ShapeManager();
        
        // Create Context For DB
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseNpgsql("Host=localhost;Database=chifoumi_db;Username=admin;Password=admin")
            .Options;
        _context = new AppDbContext(options);
        
        //Search for player and add it if not exist
        var player = _context.Players.FirstOrDefault(p => p.Name == _playerName);
        if (player == null)
        {
            player = new Player(_playerName);
            _context.Players.Add(player);
            _context.SaveChanges();
            _player1 = player;
        }
        else
        {
            _player1 = player;
        }

        // create game session and save it in DB
        _gameSession = new GameSession()
        {
            PlayerId = player.Id,
            TotalRounds = _numberOfRounds,
            DifficultyLevel = (enDifficultyLevel)_difficulty,
        };
        _context.GameSession.Add(_gameSession);
        _context.SaveChanges();
    }
    private Form TransferAnEnumToShape(enShapeType enShape)
    {
        switch (enShape)
        {
            case enShapeType.Rond:
                return new Rond(5, "Blue");
                break;
            case enShapeType.Triangle:
                return new Triangle(3, 4, 5, "Orange");
                break;
            case enShapeType.Rectangle:
                return new Rectangle(6, 4, "Green");
                break;
            default:
                return null;
        }
    }

    private Form GetShapePlayer()
    {
        
        int Shape = 0;
        while (Shape > 3 || Shape < 1)
        {
           Console.WriteLine($"{_player1.Name}, Chose your shape :");
           Console.WriteLine("1. Rond (Circle)");
           Console.WriteLine("2. Triangle");
           Console.WriteLine("3. Rectangle");
           
           Shape = int.Parse(Console.ReadLine());
        }
        return TransferAnEnumToShape((enShapeType)Shape);
    }



    private Form GetShapeComputer()
    {
        Random rnd = new Random(); 
        enShapeType computerShape = (enShapeType)rnd.Next(1, 4);
        
        return TransferAnEnumToShape(computerShape);
    }

    private Form GetComputerShapeForHardDifficulty(int playerId)
    {
        enShapeType computerShape;
        // Get last 10 round 
        var lastGames = _context.GameHistories
            .Where(gh => gh.PlayerId == playerId)
            .OrderByDescending(gh=>gh.PlayedAt)
            .Take(10)
            .ToList();

        if (lastGames.Count == 0)
        {
            return GetShapeComputer();
        }
        
        // Sorting shapes   
        var shapesCount = lastGames.GroupBy(gh=>gh.PlayerShape)
            .Select(g=> new {Shape = g.Key, Count = g.Count()})
            .OrderByDescending(g=>g.Count)
            .ToList();
        
        
        var mostPlayedShape = shapesCount.First().Shape;

        switch (mostPlayedShape)
        {
            case enShapeType.Rond:
                return TransferAnEnumToShape(enShapeType.Rectangle);
            case enShapeType.Rectangle:
                return TransferAnEnumToShape(enShapeType.Triangle);
            case enShapeType.Triangle:
                return TransferAnEnumToShape(enShapeType.Rond);
            default:
                return GetShapeComputer();
        }
    }

    private void PrintChosenShapes(enShapeType player, enShapeType computerShape)
    {
        Console.WriteLine($"{_player1.Name}, you chose {player}.");
        Console.WriteLine($"{_computer.Name}, chose {computerShape}.");
    }

    private bool isLastRound(short round)
    {
        if (round - 1 == _gameSession.TotalRounds)
        {
            var gameHistories = _context.GameHistories
                .Where(gh => gh.GameSessionId == _gameSession.Id)
                .ToList();

            int playerWins = gameHistories.Count(gh => gh.Result.StartsWith(_gameSession.Player.Name));
            int computerWins = gameHistories.Count(gh => gh.Result.StartsWith("Computer"));


            Console.WriteLine("\n\n ----------- RESULT ----------");
            
            if (playerWins > computerWins)
            {
                Console.WriteLine("\tBravo You Win !");
            }
            else if (playerWins < computerWins)
            {
                Console.WriteLine("\tSorry You Lose !");
            }
            else
            {
                Console.WriteLine("\tIt's a Draw !");
            }

            return true;
        } 
        return false;
    }
    public void Play()
    { 
        short round = 1;
        while (!isLastRound(round))
        {
            _player1.ChooseShape(GetShapePlayer());
            if (_gameSession.DifficultyLevel == enDifficultyLevel.Hard)
            {
                _computer.ChooseShape(GetComputerShapeForHardDifficulty(_player1.Id));
            }
            else
            {
                _computer.ChooseShape(GetShapeComputer());

            }
            PrintChosenShapes(_player1.GetEnumShape(), _computer.GetEnumShape());
            string result = "";
            result = DetermineWinner();
            Console.WriteLine(result);
            SaveGameResult(result);  
            round++;
        }

    }
    


    private string DetermineWinner()
    {
        if (_player1.GetEnumShape() == enShapeType.Triangle && _computer.GetEnumShape() == enShapeType.Rectangle)
        {
            return $"{_player1.Name} wins with Triangle!";
        }
        else if (_player1.GetEnumShape() == enShapeType.Rectangle && _computer.GetEnumShape() == enShapeType.Rond)
        {
            return $"{_player1.Name} wins with Rectangle!";
        }
        else if (_player1.GetEnumShape() == enShapeType.Rond && _computer.GetEnumShape() == enShapeType.Triangle)
        {
            return $"{_player1.Name} wins with Rond!";
        }
        else if (_computer.GetEnumShape() == enShapeType.Triangle && _player1.GetEnumShape() == enShapeType.Rectangle)
        {
            return $"{_computer.Name} wins with Triangle!";
        }
        else if (_computer.GetEnumShape() == enShapeType.Rectangle && _player1.GetEnumShape() == enShapeType.Rond)
        {
            return $"{_computer.Name} wins with Rectangle!";
        }
        else if (_computer.GetEnumShape() == enShapeType.Rond && _player1.GetEnumShape() == enShapeType.Triangle)
        {
            return $"{_computer.Name} wins with Rond!";
        }
        else
        {
            return "It's a tie!";
        }    
    }
    public void SaveGameResult(string result)
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseNpgsql("Host=localhost;Database=chifoumi_db;Username=admin;Password=admin")
            .Options;
        using (var context = new AppDbContext(options))
        {

            // Add game to GameHistories table in Database
            var gameHistory = new GameHistory
            {
                PlayerId = _player1.Id, 
                PlayerShape = _player1.GetEnumShape(),
                ComputerShape = _computer.GetEnumShape(),
                Result = result,
                GameSessionId = _gameSession.Id,
                PlayedAt = DateTime.UtcNow
            };

            context.GameHistories.Add(gameHistory);
            context.SaveChanges(); 
        }
    }

}