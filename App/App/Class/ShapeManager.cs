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

    public void DisplayAll()
    {
        double totalArea = 0;
        double totalPerimeter = 0;
        if (_Forms.Count() != 0)
        {
            Console.WriteLine("\n--- Informations des Formes ---");
            foreach (var form in _Forms)
            {
                form.DisplayInfo();
                totalArea += form.CalculateArea();
                totalPerimeter += form.CalculatePerimeter();
                Console.WriteLine("----------------------------------");
            }

            Console.WriteLine($"Total des Aires: {totalArea}");
            Console.WriteLine($"Total des Périmètres: {totalPerimeter}");
        }
        else
        { 
            Console.WriteLine("No forms to display");
        }

    }
}