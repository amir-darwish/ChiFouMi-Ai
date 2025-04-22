using System;  
namespace App.Class;

public class ShapeManager
{
    public int ID { get; private set; }  
    private List<Form> _Forms;

    private static int _nextId = 1;

    public ShapeManager()
    {
        ID = _nextId++; 
        _Forms = new List<Form>();
    }

    public void AddForm(Form form)
    {
        _Forms.Add(form);
    }
    public List<Form> GetForms()
    {
        return _Forms;
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