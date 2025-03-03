using System.Reflection.Emit;

namespace App.Class;

public class Rectangle : Form
{
    private double _longueur;
    private double _largeur;

    public Rectangle(double longueur,double largeur , string color) : base("Rectangle" ,color)
    {
        _longueur = longueur;
        _largeur = largeur;
    }

    public void SetLongueur(double longueur)
    {
        _longueur = longueur;
    }

    public void setLargeur(double largeur)
    {
        _largeur = largeur;
    }

    public double getLongueur()
    {
        return _longueur;
    }

    public double getLargeur()
    {
        return _largeur;
    }

    public override double CalculateArea()
    {
        return _longueur * _largeur;
    }
    public override double CalculatePerimeter()
    {
        return 2 * (_longueur + _largeur);
    }

    public override void DisplayInfo()
    {
        Console.WriteLine($"{_name} Information :\nColor "+_color+" \n"+ "Surface : "+CalculateArea()+"\nPérimètres : "+CalculatePerimeter());
        
        Console.WriteLine("Longueur : " +_longueur+"\nLargeur : "+_largeur);
    }
    
}
