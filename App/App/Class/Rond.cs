
using App.Class;

public class Rond : Form
{
    public Rond(double Diameter, string Color) : base(Diameter, Color)
    {
        
    }

    public override void DisplayInfo()
    {
        Console.WriteLine("Rond Information : Color "+_Color+" | Diametre : "+_Diametre);

    }
}

/*class Rond
{
    private double _Diametre;

    public Rond(double diametre)
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
