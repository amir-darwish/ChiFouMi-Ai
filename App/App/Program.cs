// See https://aka.ms/new-console-template for more information

using App.Class;

class Program
{
    static void Main(string[] args)
    {
        
        Triangle Triangle1 = new Triangle(3.5,"RED");
        Rectangle Rectangle1 = new Rectangle(4.5,"White");
        Rond Rond1 = new Rond(2.5,"Black");
        

        
        Triangle1.DisplayInfo();
        Console.WriteLine("_______________________________________\n");
        Rond1.DisplayInfo();
        Console.WriteLine("_______________________________________\n");
        Rectangle1.DisplayInfo();
        
        Console.WriteLine("_______________________________________\n");
        Console.WriteLine("_______________________________________\n");

        //Change Color and Edite Methodes
        Rond1.SetColor("Blue");
        Rond1.Edite(5.5);
        Rond1.DisplayInfo();

    }
    
}
