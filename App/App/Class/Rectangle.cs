namespace App.Class;

public class Rectangle : Form
{
    public Rectangle(double Diameter,string Color):base(Diameter,Color)
    {
        
    }

    public override void DisplayInfo()
    {
        Console.WriteLine("Rectangle Information : Color "+_Color+" | Diametre : "+_Diametre);

    }
}
/*public class Rectangle
{
    private double _rectangle;

    public Rectangle(double Rectangle)
    {
        _rectangle = Rectangle;
    }

    public void Edite(double Rectangle)
    {
        _rectangle = Rectangle;
    }
    public double getDiameter()
    {
        return _rectangle;
    }
}*/