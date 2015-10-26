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
        public static void CalculateInterval(TitleType i_titleType)
        {
            DateTime begin = new DateTime(1, 1, 1);
            DateTime doom = new DateTime(1337, 1, 1);
            foreach (Title title in China.titles)
            {
                if (title.TitleType == i_titleType)
                {
                    if (title.fictional)
                    {
                        if (title.captial != null && title.captial.TitleType != TitleType.Baron && title.captial.TitleType != TitleType.Emporer)
                        {
                            bool isLiegeFound = false;
                            foreach (KeyValuePair<DateTime, DateTime> entry in title.captial.Intervals)
                            {
                                title.Intervals.Add(entry.Key, entry.Value);
                                if (!isLiegeFound)
                                {
                                    isLiegeFound = true;
                                    if (title.liege != null)
                                    {
                                        title.lieges.Add(entry.Key, rootNode);
                                    }
                                }
                                Person temp = new Person();
                                temp.id = 0;
                                if (entry.Value != doom)
                                {
                                    title.history.Add(entry.Value, temp);
                                }
                            }
                        }
                        else
                        {
                            title.Intervals.Add(begin, doom);
                        }
                    }
                    else
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
    }
}
