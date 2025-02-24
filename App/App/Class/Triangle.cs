namespace App.Class;

public class Triangle : Form
{
   public Triangle(double diameter, string color) : base(diameter, color)
    {
        
    }

    public override void DisplayInfo()
    {
        Console.WriteLine("Triangle Information : Color "+_Color+" | Diametre : "+_Diametre);
    }
}

/*public class Triangle
{
    private double _Diametre;

    public Triangle(double diametre)
    {
        _Diametre = diametre;
    }

    public void Edite(double diametre)
    {
        _Diametre = diametre;
    }
    public double getDiametre()
    {
        return _Diametre;
    }
}*/