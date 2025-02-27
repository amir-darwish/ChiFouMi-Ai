// See https://aka.ms/new-console-template for more information

using App.Class;

class Program
{
    static void Main(string[] args)
    {
        ShapeManager groupe1 = new ShapeManager();
        groupe1.AddForm(new Triangle(24,5,3,"RED"));
        groupe1.AddForm(new Rond(25,6,"BLUE"));
        groupe1.AddForm(new Rectangle(25,3,"WHITE"));
        groupe1.DisplayAll();
        
        /*Triangle Triangle1 = new Triangle(4,7,12,"RED");
        Rectangle rectangle = new Rectangle(4.5,7.9,"White");
        Rond Rond1 = new Rond(2.5,4.2,"Black");
        
        Triangle1.DisplayInfo();
        Console.WriteLine("_______________________________________\n");
        Rond1.DisplayInfo();
        Console.WriteLine("_______________________________________\n");
        rectangle.DisplayInfo();
        Console.WriteLine("_______________________________________\n");

        Triangle1.setAdjacent(3);
        Triangle1.DisplayInfo();*/

    }
    
}
