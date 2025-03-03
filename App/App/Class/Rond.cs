
using System.ComponentModel;
using App.Class;

public class Rond : Form
{
    private double _rayon;
    private double _centre;
    public Rond(double centre, double rayon, string color) : base("Rond" ,color)
    {
        _centre = centre;
        _rayon = rayon;
    }

    public void setRayon(double rayon)
    {
        _rayon = rayon;
    }

    public void setCentre(double centre)
    {
        _centre = centre;
    }

    public double getRayon()
    {
        return _rayon;
    }

    public double getCentre()
    {
        return _centre;
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
        Console.WriteLine("Le Centre : " +_centre+"\n"+ "Le Rayon : "+_rayon);
    }

}

