using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmbraChinaCK2
{
    class LoadDynasties
    {
        static string name = "name";
        static string culture = "culture";
        static string han = "han";
        static public bool LoadDynastiesFromFile(string i_path)
        {
            if (!File.Exists(i_path))
            {
                return false;
            }

            try
            {
                // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader(i_path, System.Text.Encoding.GetEncoding("ISO8859-1")))
                {
                    // Read the stream to a string, and write the string to the console.
                    String line = sr.ReadToEnd();
                    if (!ParseDynastyFile(line))
                    {
                        return false;
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

        static bool ParseDynastyFile(string i_content)
        {
            using (StringReader sr = new StringReader(i_content))
            {
                while (sr.Peek() != -1)
                {
                    Reader.ReadSpaces(sr);
                    string key;
                    if (!Reader.ReadAKey(sr, out key))
                    {
                        Debug.Assert(false, "A key expected");
                        return false;
                    }
                    Reader.ReadSpaces(sr);
                    if (!Reader.ReadAToken(sr, '='))
                    {
                        Debug.Assert(false, "A token '=' expected");
                        return false;
                    }
                    Reader.ReadSpaces(sr);
                    int id = Int32.Parse(key);
                    Dynasty dynasty = new Dynasty();
                    dynasty.id = id;
                    ParseDynasty(sr, dynasty);
                    if (dynasty.culture == han)
                    {
                        China.dynasties.Add(dynasty);
                    }
                    Reader.ReadSpaces(sr);
                }
            }
            return true;
        }
        static bool ParseDynasty(StringReader i_sr, Dynasty i_dynasty)
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
                if (key == name)
                {
                    string strname;
                    if (!Reader.ReadAString(i_sr, out strname))
                    {
                        Debug.Assert(false, "A string expected");
                    }
                    i_dynasty.name = strname;
                }
                else if (key == culture)
                {
                    string strculture;
                    if (!Reader.ReadAKey(i_sr, out strculture))
                    {
                        Debug.Assert(false, "A string expected");
                    }
                    i_dynasty.culture = strculture;
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
