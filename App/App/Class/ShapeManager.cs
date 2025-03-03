using System;  
namespace App.Class;

public class ShapeManager
{
    private new List<Form> _Forms;

    public ShapeManager()
    {
        _Forms = new List<Form>();
    }

    public void AddForm(Form form)
    {
        _Forms.Add(form);
    }

    public double CalculateTotalArea()
    {
        double totalArea = 0;
        foreach (var form in _Forms)
        {
            totalArea += form.CalculateArea();
        }
        return totalArea;
    }

    public double CalculateTotalPerimeter()
    {
        double totalPerimeter = 0;
        foreach (var form in _Forms)
        {
            totalPerimeter += form.CalculatePerimeter();
        }
        return totalPerimeter;
    }
    public void DisplayAll()
    {

        if (_Forms.Count() != 0)
        {
            Console.WriteLine("\n--- Informations des Formes ---");
            foreach (var form in _Forms)
            {
                form.DisplayInfo();

                Console.WriteLine("----------------------------------");
            }

            Console.WriteLine($"Total des Aires: {CalculateTotalArea()}");
            Console.WriteLine($"Total des Périmètres: {CalculateTotalArea()}");
        }
        else
        { 
            Console.WriteLine("No forms to display");
        }

    }
}