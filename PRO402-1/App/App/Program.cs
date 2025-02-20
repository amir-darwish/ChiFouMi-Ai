// See https://aka.ms/new-console-template for more information

using App.Class;

class Program
{
    static void Main(string[] args)
    {        
        
        Triangle Triangle1 = new Triangle(3.5);
        Carre Carre1 = new Carre(4.5);
        Rond Rond1 = new Rond(2.5);
        
        Console.WriteLine("Diametre de Triangle : "+ Triangle1.getDiametre());
        Console.WriteLine("_______________________________________\n");
        Console.WriteLine("Diametre de carre : "+ Carre1.getCarre());
        Console.WriteLine("_______________________________________\n");
        Console.WriteLine("Diametre de rond : "+Rond1.getDiametre());
    }
}
