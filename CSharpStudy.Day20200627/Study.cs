using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpStudy.Day20200627
{
    [Serializable]
    public class Study
    {
        string name;

        [NonSerialized]
        string location;

        public Study(string name, string location)
        {
            this.name = name;
            this.location = location;
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Location
        {
            get { return location; }
            set { location = value; }
        }

        public override string ToString()
        {
            return string.Format("Name={0}, Location={1}", name, location);
        }
    }
}
