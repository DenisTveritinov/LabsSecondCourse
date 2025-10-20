using System;

namespace ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8; // чтобы корректно показывать українські літери

            var menu = new ConsoleMenu();
            menu.start();

            Console.WriteLine("\nПрограма завершена. Натисніть будь-яку клавішу...");
            Console.ReadKey();
        }
    }
}
