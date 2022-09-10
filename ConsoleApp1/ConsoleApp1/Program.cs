using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int sum = 0;
            for (int i =0; i<5; i++)
            {
                int number = i + 2;
                sum += number / (number / 2);
                Console.WriteLine(sum);
                Console.ReadLine();
            }
        }
    }
}
