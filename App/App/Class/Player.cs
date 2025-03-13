namespace App.Class;

public class Player
{
    public string Name { get; set; }
    private Form ChosenShape { get; set; }

    public Player(string name)
    {
        Name = name;
    }

    public void ChooseShape(Form shape)
    {
        ChosenShape = shape;
    }
    
    public enShapeType GetEnumShape()
    {
        switch (ChosenShape)
        {
            case Triangle:
                return enShapeType.Triangle;
                break;
            case Rectangle:
                return enShapeType.Rectangle;
                break;
            default:
                return enShapeType.Rond;

        }
    }
}