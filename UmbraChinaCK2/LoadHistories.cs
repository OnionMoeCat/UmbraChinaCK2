using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmbraChinaCK2
{
    class LoadHistories
    {
        static private string liege = "liege";
        static private string holder = "holder";
        static private string rootTitle = "e_china";    
        static public bool LoadHistoriesFromFolder(string i_path)
        {
            if (!Directory.Exists(i_path))
            {
                return false;
            }

            try
            {   
                foreach(Title title in China.titles)
                {
                    if (title.TitleType != TitleType.Baron && title.TitleType != TitleType.Emporer )
                    {
                        string path = Path.Combine(i_path, title.name + ".txt");
                        if (File.Exists(path))
                        {
                            // Open the text file using a stream reader.
                            using (StreamReader sr = new StreamReader(path, System.Text.Encoding.GetEncoding("ISO8859-1")))
                            {
                                // Read the stream to a string, and write the string to the console.
                                String line = sr.ReadToEnd();
                                if (!ParseHistoryFile(line, title))
                                {
                                    return false;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Debug.Assert(false, "The file could not be read:");
                Debug.Assert(false, e.Message);
            }

            return true;
        }
        static bool ParseHistoryFile(string i_content, Title title)
        {
            bool isChina = false;
            bool isEmpty = false;
            DateTime doom = new DateTime(1337,1,1);
            using (StringReader sr = new StringReader(i_content))
            {
                while (sr.Peek() != -1)
                {
                    Reader.ReadSpaces(sr);
                    DateTime DateTime;
                    if (!Reader.ReadADateTime(sr, out DateTime))
                    {
                        Debug.Assert(false, "A DateTime expected");
                        return false;
                    }
                    Reader.ReadSpaces(sr);
                    if (!Reader.ReadAToken(sr, '='))
                    {
                        Debug.Assert(false, "A token '=' expected");
                        return false;
                    }
                    Reader.ReadSpaces(sr);
                    ParseHistory(sr, title, DateTime, ref isChina, ref isEmpty);
                    Reader.ReadSpaces(sr);
                }
                
                title.DateTimes.Add(doom, "");
            }
            return true;
        }
        static bool ParseHistory(StringReader i_sr, Title title, DateTime DateTime, ref bool isChina, ref bool isEmpty)
        {
            Reader.ReadSpaces(i_sr);
            if (!Reader.ReadAToken(i_sr, '{'))
            {
                Debug.Assert(false, "A token '{' expected");
                return false;
            }
            Reader.ReadSpaces(i_sr);
            while (i_sr.Peek() != '}')
            {
                Reader.ReadSpaces(i_sr);
                string key;
                if (!Reader.ReadAKey(i_sr, out key))
                {
                    Debug.Assert(false, "A key expected");
                    return false;
                }

                Reader.ReadSpaces(i_sr);
                if (!Reader.ReadAToken(i_sr, '='))
                {
                    Debug.Assert(false, "A token '=' expected");
                    return false;
                }
                Reader.ReadSpaces(i_sr);
                Debug.Assert(key == liege || key == holder);
                if (key == liege)
                {
                    if (i_sr.Peek() == '"')
                    {
                        string strliege;
                        if (!Reader.ReadAString(i_sr, out strliege))
                        {
                            Debug.Assert(false, "A string expected");
                        }
                        if (!title.DateTimes.ContainsKey(DateTime))
                        {
                            title.DateTimes.Add(DateTime, strliege);
                        }
                        isChina = strliege == rootTitle;
                        title.lieges.Add(DateTime, strliege);
                    }
                    else
                    {
                        string strliege;
                        if (!Reader.ReadAKey(i_sr, out strliege))
                        {
                            Debug.Assert(false, "A key expected");
                        }
                        if (!title.DateTimes.ContainsKey(DateTime))
                        {
                            title.DateTimes.Add(DateTime, "");
                        }
                        title.lieges.Add(DateTime, "0");
                    }               
                }
                else if (key == holder)
                {
                    string strholder;
                    if (!Reader.ReadAKey(i_sr, out strholder))
                    {
                        Debug.Assert(false, "A key expected");
                    }
                    int id = Int32.Parse(strholder);
                    if (id == 0)
                    {
                        if (!title.DateTimes.ContainsKey(DateTime))
                        {
                            title.DateTimes.Add(DateTime, "");
                        }
                        Person person = new Person();
                        person.id = id;
                        title.history.Add(DateTime, person);
                        isEmpty = true;
                    }
                    else
                    {
                        if (isEmpty)
                        {
                            KeyValuePair<DateTime, string> lastTwoKeyValuePair = title.DateTimes.ElementAt(title.DateTimes.Count - 2);
                            isChina = lastTwoKeyValuePair.Value == rootTitle;                                                                                     
                            if (isChina)
                            {
                                if (!title.DateTimes.ContainsKey(DateTime))
                                {
                                    title.DateTimes.Add(DateTime, rootTitle);
                                }
                            }
                            isEmpty = false;
                        }
                        if (!isChina)
                        {
                            Person person = new Person();
                            person.id = id;
                            title.history.Add(DateTime, person);
                        }                
                    }
                }
                else
                {
                    Reader.ReadValue(i_sr);
                }
                Reader.ReadSpaces(i_sr);

            }
            if (!Reader.ReadAToken(i_sr, '}'))
            {
                Debug.Assert(false, "A token '}' expected");
                return false;
            }
            return true;
        }
    }
}
