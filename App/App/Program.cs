// See https://aka.ms/new-console-template for more information

using App.Class;
using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter your name:");
        string playerName = Console.ReadLine();
        Player player1 = new Player(playerName);

        Game game = new Game(player1);
        game.Play();
        
        
        
    }
    
}
