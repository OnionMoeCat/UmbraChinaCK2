using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmbraChinaCK2
{
    class GenerateDynasties
    {
        public static void Gen()
        {
            int count = 0;
            int lenDyn = China.dynasties.Count();
            foreach(Title title in China.titles)
            {
                if (title.TitleType == TitleType.Count)
                {
                    int index = count % lenDyn;
                    Dynasty dynasty = new Dynasty();
                    dynasty.name = China.dynasties[index].name;
                    count++;
                }
            }
        }
    }
}
