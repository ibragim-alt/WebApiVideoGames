using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Person person = new Person();
            person.SetName("");
            Console.WriteLine(person.GetName());
            Console.ReadLine(); 
        }
    }
}
