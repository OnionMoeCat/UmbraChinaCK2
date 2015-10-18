using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmbraChinaCK2
{
    class Dynasty
    {
        public int id;
        public string name;
        public string culture;
    }
    class Name
    {
        public string first;
        public string middle;
        public Dynasty dynasty;
    }
    class Person
    {
        public int id;
        public bool fictional;
        public Person father;
        public List<Person> children;
        public Time born;
        public Time die;
        public Name name;
        public Dynasty dynasty
        {
            get
            {
                return name.dynasty;
            }
        }
    }
}
