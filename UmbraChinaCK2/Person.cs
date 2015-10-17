using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmbraChinaCK2
{
    class Family
    {
        public string name;
    }
    class Name
    {
        public string first;
        public string middle;
        public Family family;
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
        public Family family
        {
            get
            {
                return name.family;
            }
        }
    }
}
