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
            using (StringReader sr = new StringReader(i_content))
            {
                while (sr.Peek() != -1)
                {
                    Reader.ReadSpaces(sr);
                    Time time;
                    if (!Reader.ReadATime(sr, out time))
                    {
                        Debug.Assert(false, "A time expected");
                        return false;
                    }
                    Reader.ReadSpaces(sr);
                    if (!Reader.ReadAToken(sr, '='))
                    {
                        Debug.Assert(false, "A token '=' expected");
                        return false;
                    }
                    Reader.ReadSpaces(sr);
                    ParseHistory(sr, title, time);
                    Reader.ReadSpaces(sr);
                }
            }
            return true;
        }
        static bool ParseHistory(StringReader i_sr, Title title, Time time)
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
                if (key == liege)
                {
                    if (i_sr.Peek() == '"')
                    {
                        string strliege;
                        if (!Reader.ReadAString(i_sr, out strliege))
                        {
                            Debug.Assert(false, "A string expected");
                        }
                        title.times.Add(time, strliege);
                    }
                    else
                    {
                        string strliege;
                        if (!Reader.ReadAKey(i_sr, out strliege))
                        {
                            Debug.Assert(false, "A string expected");
                        }
                        title.times.Add(time, "");
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
