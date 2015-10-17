using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmbraChinaCK2
{
    class LoadTitles
    {
        static string rootTitle = "e_china";
        static bool LoadTitlesFromFile(string i_path)
        {
            if (!File.Exists(i_path))
            {
                return false;
            }

            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader(i_path))
                {
                    // Read the stream to a string, and write the string to the console.
                    String line = sr.ReadToEnd();
                    if (!ParseTitleFile(line))
                    {
                        return false;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

            return true;
        }
        static bool ParseTitleFile(string i_content)
        {
            using (StringReader sr = new StringReader(i_content))
            {
                while (sr.Peek() != -1)
                {
                    string key;
                    if (!Reader.ReadAKey(sr, out key))
                    {
                        Debug.Assert(false, "A key expected");
                    }
                    if (!Reader.ReadAToken('='))
                    {
                        Debug.Assert(false, "A token '=' expected");
                    }

                    if (key == rootTitle)
                    {
                        Title root = new Title();
                        root.name = rootTitle;
                        China.titles.Add(root);
                        ParseRootTitle(sr, root);
                    }
                    else
                    {
                        ParseValue(sr);
                    }
                }
            }
        }

        static bool ParseRootTitle(StringReader i_sr, Title i_parent)
        {
            if (!Reader.ReadAToken('{'))
            {
                Debug.Assert(false, "A token '{' expected");
            }

            if (!Reader.ReadAToken('}'))
            {
                Debug.Assert(false, "A token '}' expected");
            }
            string key;
        }
    }
}
