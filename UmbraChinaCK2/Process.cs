using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmbraChinaCK2
{
    class Process
    {
        static string rootNode = "e_china";
        public static void CalculateInterval()
        {
            DateTime begin = new DateTime(1, 1, 1);
            DateTime doom = new DateTime(1337, 1, 1);
            foreach (Title title in China.titles)
            {
                if (title.DateTimes.Count > 0)
                {
                    bool isOn = false;
                    DateTime beginDateTime = new DateTime();
                    foreach (KeyValuePair<DateTime, string> entry in title.DateTimes)
                    {
                        if (entry.Value == rootNode)
                        {
                            isOn = true;
                            beginDateTime = entry.Key;
                        }
                        else
                        {
                            if (isOn)
                            {
                                isOn = false;
                                title.Intervals.Add(beginDateTime, entry.Key);
                            }
                        }
                    }
                }
                else
                {
                    title.Intervals.Add(begin, doom);
                }
            }
        }
    }
}
