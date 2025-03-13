namespace App.Class;

public class Game
{
    private Player _player1;
    private Player _computer;
    private ShapeManager _shapeManager;

    public Game(Player player1)
    {
        _player1 = player1;
        _computer = new Player("Computer");
        _shapeManager = new ShapeManager();
        
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

    private void PrintChosenShapes(enShapeType player, enShapeType computerShape)
    {
        Console.WriteLine($"{_player1.Name}, you chose {player}.");
        Console.WriteLine($"{_computer.Name}, chose {computerShape}.");
    }
    public void Play()
    { 
 
        _player1.ChooseShape(GetShapePlayer());        
       _computer.ChooseShape(GetShapeComputer());
       PrintChosenShapes(_player1.GetEnumShape(), _computer.GetEnumShape());
        string result = "";
        result = DetermineWinner();
        Console.WriteLine(result);
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
        }    }
}