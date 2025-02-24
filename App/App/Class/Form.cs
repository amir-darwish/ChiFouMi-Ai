namespace App.Class;

public abstract class Form
{
    protected double _Diametre;
    protected string _Color;

    public Form (double diametre, string color)
    {
        _Diametre = diametre;
        _Color = color;
    }
    public void SetDiametre(double diametre)
    {
        _Diametre = diametre;
    }

    public void SetColor(string color)
    {
        _Color = color;
    }

    public virtual void Edite(double diametre)
    {
        _Diametre = diametre;
    }

    public string getColor()
    {
        return _Color;
    }

    public double getDiametre()
    {
        return _Diametre;
    }
    public abstract void DisplayInfo();
    
}