using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpStudy.Day20200627
{
    public class Person
    {
        public string Name;
        public int Age;
        
        public Person()
        {

        }
        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public override string ToString()
        {
            return string.Format("Name={0}, Age={1}", Name, Age);
        }
    }
}
