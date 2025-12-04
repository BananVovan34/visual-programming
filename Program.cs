using visualprogramming.AiMethodsLab4;
using visualprogramming.CounterMine;
using visualprogramming.lab2;
using visualprogramming.Lab3;
using visualprogramming.Lab4;
using visualprogramming.Lab5;
using visualprogramming.Lab6;
using visualprogramming.Lab7;
using visualprogramming.Lab8;
using visualprogramming.Lab9;
using windowsforms;

namespace visualprogramming
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            
            // Lab1
            //ApplicationConfiguration.Initialize();
            //Application.Run(new Form1());
            
            // Lab2
            //TestLab2.Test();
            
            // Lab3
            //TestLab3.Test();
            
            // Lab4
            //TestLab4.Test();

            // Lab5
            //Application.Run(new LockerForm());

            // Lab6
            //Application.Run(new PlayingCard());
            
            // Lab7
            //Application.Run(new LengthDBForm());

            // Lab8
            //Application.Run(new RecipeLoader());
            
            // AiMethods Lab4
            //Application.Run(new Recognizer());
            
            //LevelProgressionModel.RunSimulation();
            
            ApplicationConfiguration.Initialize();
            Application.Run(new MultiThreadGraphics());
        }
    }
}