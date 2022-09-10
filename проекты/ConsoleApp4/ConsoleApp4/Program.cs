using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var list = new List<int>();
            for(int i = 0; i < 15; i++)
            {
                list.Add(i);
            }
            int sum = list.ToArray().Sum();
            Console.WriteLine("Сумма: " + sum);
            Console.ReadKey();

            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine(list[i]);
            }

            Console.ReadLine();
             

            
        }
    }
}
