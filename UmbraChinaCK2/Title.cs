using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmbraChinaCK2
{
    enum TitleType
    {
        Baron,
        Count,
        Duke,
        King,
        Emporer
    }
    class Title
    {
        public List<Title> vassals;
        public Title liege;
        public SortedList<Time, Person> history;
        public Title captial
        {
            get {
                if (vassals.Count > 0)
                {
                    return vassals[0];
                }
                return null;
            }
        }
        public string name;
    }
}
