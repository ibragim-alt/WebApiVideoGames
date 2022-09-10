using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp6
{
    public class Person
    {
        private string _name;


        public string GetName()
        {
             return _name; 
        }
        public void SetName(string name)
        {
            if(string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("Имя не может быть пустым");
            }

            _name = name;
        }

        

    }
}
