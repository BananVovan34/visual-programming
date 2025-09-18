using visualprogramming.lab2;
using visualprogramming.Lab3;
using visualprogramming.Lab4;
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
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
            
            // Lab2
            TestLab2.Test();
            
            // Lab3
            TestLab3.Test();
            
            // Lab4
            TestLab4.Test();
        }
    }
}