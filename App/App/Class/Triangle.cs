namespace App.Class;

public class Triangle : Form
{
    private double _adjacent;
    private double _oppose;
    private double _hypotenuse;
   public Triangle(double adjacent,double oppose,double hypotenuse , string color) : base("Triangle" ,color)
    {
        _adjacent = adjacent;
        _oppose = oppose;
        _hypotenuse = hypotenuse;
    }

    public void setAdjacent(double adjacent)
    {
        _adjacent = adjacent;
    }

    public void setOppose(double oppose)
    {
        _oppose = oppose;
    }

    public void setHypotenuse(double hypotenuse)
    {
        _hypotenuse = hypotenuse;
    }

    public double getAdjacent()
    {
        return _adjacent;
    }

    public double getOppose()
    {
        return _oppose;
    }

    public double getHypotenuse()
    {
        return _hypotenuse;
    }
    public override double CalculateArea()
    {
        return 0.5 * _adjacent * _oppose;

    }
    public override double CalculatePerimeter()
    {
        return _adjacent + _oppose + _hypotenuse;
    }

    public override void DisplayInfo()
    {
        Console.WriteLine($"{_name} Information :\nColor "+_color+" \n"+ "Surface : "+CalculateArea()+"\nPérimètres : "+CalculatePerimeter());
        Console.WriteLine("Côté adjacent : " +_adjacent+"\nCôté opposé : "+_oppose+"\nHypoténuse :"+_hypotenuse);
    } 

    
}

