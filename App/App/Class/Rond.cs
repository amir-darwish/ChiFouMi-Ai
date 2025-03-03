
using System.ComponentModel;
using App.Class;

public class Rond : Form
{
    private double _rayon;
    public Rond( double rayon, string color) : base("Rond" ,color)
    {
        _rayon = rayon;
    }

    public void setRayon(double rayon)
    {
        _rayon = rayon;
    }



    public double getRayon()
    {
        return _rayon;
    }


    public override double CalculateArea()
    {
        return Math.PI * _rayon * _rayon;
    }
    public override double CalculatePerimeter()
    { 
        return 2 * Math.PI * _rayon;
    }

    
    public override void DisplayInfo()
    {
        Console.WriteLine($"{_name} Information :\nColor "+_color+" \n"+ "Surface : "+CalculateArea()+"\nPérimètres : "+CalculatePerimeter());
        Console.WriteLine("Le Rayon : "+_rayon);
    }

}

