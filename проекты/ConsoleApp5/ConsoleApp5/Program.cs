using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите радиус: ");
            var radius = double.Parse(Console.ReadLine());
            Console.WriteLine($"Площадь круга: {Math.PI * radius * radius}");
            Console.ReadLine();

        }
    }
}
