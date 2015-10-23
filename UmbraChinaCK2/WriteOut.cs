using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmbraChinaCK2
{
    class WriteOut
    {
        private static string holder = "holder";
        private static string liege = "liege";
        private static string cultureStr = "culture";
        private static string nameStr = "name";
        private static string dynastyStr = "dynasty";
        private static string birthStr = "birth";
        private static string dieStr = "die";
        public static void OutputTitles(string i_folder)
        {
            foreach (Title title in China.titles)
            {
                List<KeyValuePair<DateTime, KeyValuePair<string, string>>> totalList = new List<KeyValuePair<DateTime, KeyValuePair<string, string>>>();
                foreach (KeyValuePair<DateTime, Person> entry in title.history)
                {
                    totalList.Add(new KeyValuePair<DateTime, KeyValuePair<string, string>>(entry.Key, new KeyValuePair<string, string>(holder ,"" + entry.Value.id)));
                }
                foreach (KeyValuePair<DateTime, string> entry in title.lieges)
                {
                    totalList.Add(new KeyValuePair<DateTime, KeyValuePair<string, string>>(entry.Key, new KeyValuePair<string, string>(liege, entry.Value)));
                }

                PrintToTitleFile(Path.Combine(i_folder, title.name + ".txt"), totalList);
            }

        }

        public static void PrintToTitleFile(string i_path, List<KeyValuePair<DateTime, KeyValuePair<string, string>>> i_totalList)
        {
            i_totalList.Sort();
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(i_path))
            {
                foreach (KeyValuePair<DateTime, KeyValuePair<string, string>> entry in i_totalList)
                {
                    DateTime date = entry.Key;
                    string key = entry.Value.Key;
                    string value = entry.Value.Value;
                    file.WriteLine(DateTimeToString(date) + " ={");
                    file.WriteLine("\t" + key + " = " + value);
                    file.WriteLine("}");
                    file.WriteLine();
                }
            }
        }

        public static void OutputDynasties(string i_path)
        {           
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(i_path))
            {
                foreach (Dynasty entry in China.dynasties)
                {
                    int id = entry.id;
                    string name = entry.name;
                    string culture = entry.culture;
                    file.WriteLine("" + id + " ={");
                    file.WriteLine("\t" + nameStr + " = \"" + name + "\"");
                    file.WriteLine("\t" + cultureStr +" = \"" + name + "\"");
                    file.WriteLine("}");
                    file.WriteLine();
                }
            }
        }

        public static void OutputPeople(string i_path)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(i_path))
            {
                foreach (Person entry in China.people)
                {
                    int id = entry.id;
                    string name = entry.name.middle + entry.name.first;
                    int dynasty = entry.dynasty.id;
                    string religion = "confucian";
                    string culture = entry.dynasty.culture;
                    int father = (entry.father != null) ? entry.father.id : 0;
                    DateTime born = entry.born;
                    DateTime die = entry.die;
                    file.WriteLine("" + id + " ={");
                    file.WriteLine("\t" + nameStr + " = \"" + name + "\"");
                    file.WriteLine("\t" + dynastyStr + " = " + dynasty);
                    file.WriteLine("\t" + cultureStr + " = \"" + culture + "\"");
                    file.WriteLine("\t" + DateTimeToString(born) + " ={");
                    file.WriteLine("\t\t" + birthStr + " = \"" + DateTimeToString(born) + "\"");
                    file.WriteLine("\t}");
                    file.WriteLine("\t" + DateTimeToString(die) + " ={");
                    file.WriteLine("\t\t" + dieStr + " = \"" + DateTimeToString(die) + "\"");
                    file.WriteLine("\t}");
                    file.WriteLine("}");
                    file.WriteLine();
                }
            }
        }

        public static string DateTimeToString(DateTime i_dateTime)
        {
            return i_dateTime.Year + "." + i_dateTime.Month + "." + i_dateTime.Day;
        }
    }
}
