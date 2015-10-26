using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmbraChinaCK2
{
    class GenerateDynasties
    {
        static int dynastyIdBase = 1000156100;
        static int personIdBase = 1000451000;
        static string rootNode = "e_china";
        static int dynastyNum = 0;
        static int personNum = 0;
        static int minInterval = 20;
        static int maxInterval = 30;
        static int minElectedAge = 17;
        static int maxElectedAge = 30;
        static int maxDeathTime = 60;
        static int adultYear = 16;
        static string han = "han";
        static string tao = "tao";
        static string confucian = "confucian";
        static DateTime taoToConfucian = new DateTime(930, 1, 1);

        public static void GenCount()
        {            
            foreach (Title title in China.titles)
            {
                if (title.TitleType == TitleType.Count)
                {
                    foreach (KeyValuePair<DateTime, DateTime> entry in title.Intervals)
                    {
                        GenCharacter(title, entry.Key, entry.Value);
                    }   
                }
            }
            dynastyNum = 0;
        }

        public static void GenDuke()
        {
            DateTime doom = new DateTime(1337, 1, 1);
            foreach (Title title in China.titles)
            {
                if (title.TitleType == TitleType.Duke)
                {
                    if (title.captial != null)
                    {
                        for (int i = 0; i < title.captial.history.Count; i ++)
                        {
                            KeyValuePair<DateTime, Person> current = title.captial.history.ElementAt(i);
                            if (current.Value.name != null)
                            {
                                DateTime beginTime = current.Key;
                                Person person = current.Value;
                                DateTime endTime;
                                if (i == title.captial.history.Count - 1)
                                {
                                    endTime = doom;
                                }
                                else
                                {
                                    endTime = title.captial.history.ElementAt(i + 1).Key;
                                }
                                foreach (KeyValuePair<DateTime, DateTime> entry in title.Intervals)
                                {
                                    if (Intersect(beginTime, endTime, entry.Key, entry.Value))
                                    {
                                        title.history.Add(beginTime, person);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public static void GenKing()
        {
            DateTime doom = new DateTime(1337, 1, 1);
            foreach (Title title in China.titles)
            {
                if (title.TitleType == TitleType.King)
                {
                    if (title.captial != null)
                    {
                        for (int i = 0; i < title.captial.history.Count; i++)
                        {
                            KeyValuePair<DateTime, Person> current = title.captial.history.ElementAt(i);
                            if (current.Value.name != null)
                            {
                                DateTime beginTime = current.Key;
                                Person person = current.Value;
                                DateTime endTime;
                                if (i == title.captial.history.Count - 1)
                                {
                                    endTime = doom;
                                }
                                else
                                {
                                    endTime = title.captial.history.ElementAt(i + 1).Key;
                                }
                                foreach (KeyValuePair<DateTime, DateTime> entry in title.Intervals)
                                {
                                    if (Intersect(beginTime, endTime, entry.Key, entry.Value))
                                    {
                                        title.history.Add(beginTime, person);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public static bool Intersect(DateTime beginTime1, DateTime endTime1, DateTime beginTime2, DateTime endTime2)
        {
            return !(endTime1 <= beginTime2 || endTime2 <= beginTime1);
        }

        public static void GenCharacter(Title title, DateTime i_beginDateTime, DateTime i_endDateTime)
        {
            Dynasty dynasty = GenDynasty();
            DateTime currentDateTime = i_beginDateTime;
            Person father = null;
            while (currentDateTime < i_endDateTime)
            {
                int intervalYears = Rnd.rnd.Next(minInterval, maxInterval + 1);                
                DateTime afterTime = new DateTime(currentDateTime.Year + intervalYears, Rnd.rnd.Next(1, 13), Rnd.rnd.Next(1, 29));
                Person person = GenPerson(dynasty, father, currentDateTime, afterTime);
                title.history.Add(currentDateTime, person);
                currentDateTime = afterTime;
                father = person;
            }
            dynastyNum++;
        }

        public static Dynasty GenDynasty()
        {
            int lenDyn = China.dynasties.Count();
            int index = dynastyNum % lenDyn;
            Dynasty dynasty = new Dynasty();
            dynasty.name = China.dynasties[index].name;
            dynasty.id = dynastyIdBase + dynastyNum;
            dynasty.culture = han;
            China.dynasties.Add(dynasty);
            return dynasty;
        }

        public static Person GenPerson(Dynasty i_dynasty, Person i_father, DateTime i_beginDateTime, DateTime i_endDateTime)
        {        
            Person person = new Person();
            if (i_beginDateTime > taoToConfucian)
            {
                person.religion = confucian;
            }
            else
            {
                person.religion = tao;
            }
            person.father = i_father;
            person.name = new Name();
            person.name.first = Name.RandomName();
            DateTime born = new DateTime(Rnd.rnd.Next(i_beginDateTime.Year -maxElectedAge, i_beginDateTime.Year - minElectedAge + 1), Rnd.rnd.Next(1, 13), Rnd.rnd.Next(1, 29));                        
            if (i_father != null)
            {
                DateTime fatherBorn = i_father.born;
                DateTime minBornTime = i_father.born.AddYears(adultYear);
                if (minBornTime > born)
                {
                    born = new DateTime(Rnd.rnd.Next(minBornTime.Year + 1, minBornTime.Year + 1 + 5), Rnd.rnd.Next(1, 13), Rnd.rnd.Next(1, 29));
                    born = new DateTime(Math.Min(born.Ticks, i_father.die.AddDays(-1).Ticks));
                    born = new DateTime(Math.Min(born.Ticks, i_beginDateTime.AddYears(-16).Ticks));                    
                }
                i_father.children.Add(person);
            }
            DateTime maxTime = born.AddYears(maxDeathTime);
            DateTime die = new DateTime(Rnd.rnd.Next(i_endDateTime.Year, maxTime.Year + 1), Rnd.rnd.Next(1, 13), Rnd.rnd.Next(1, 29));
            die = new DateTime(Math.Max(die.Ticks, i_endDateTime.AddDays(1).Ticks));
            Debug.Assert(i_father == null || born < i_father.die);
            Debug.Assert(born.AddYears(adultYear) <= i_beginDateTime);
            Debug.Assert(die >= i_endDateTime);
            person.born = born;
            person.die = die;
            if (Rnd.rnd.Next(0, 4) == 0)
            {
                person.name.middle = "";
            }
            else
            {
                person.name.middle = Name.RandomName();
            }
            person.name.dynasty = i_dynasty;
            person.id = personIdBase + personNum;
            China.people.Add(person);
            personNum++;
            return person;
        }
    }
}
