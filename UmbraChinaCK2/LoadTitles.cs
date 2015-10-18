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
        static public bool LoadTitlesFromFile(string i_path)
        {
            if (!File.Exists(i_path))
            {
                return false;
            }

            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader(i_path, System.Text.Encoding.GetEncoding("ISO8859-1")))
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
                    if (key == rootTitle)
                    {
                        Title root = new Title();
                        root.name = rootTitle;
                        China.titles.Add(root);
                        ParseTitle(sr, root);
                    }
                    else
                    {
                        ParseValue(sr);
                    }
                    Reader.ReadSpaces(sr);
                }
            }
            return true;
        }

        static bool ParseTitle(StringReader i_sr, Title i_parent)
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
                if (Title.IsTitle(key))
                {
                    Title title = new Title();
                    title.name = key;
                    Title.BuildVassal(i_parent, title);
                    China.titles.Add(title);
                    ParseTitle(i_sr, title);
                }
                else
                {
                    ParseValue(i_sr);
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

        static bool ParseValue(StringReader i_sr)
        {
            Reader.ReadSpaces(i_sr);
            if (i_sr.Peek() == '{')
            {
                int layer = 0;                             
                do
                {
                    if (i_sr.Peek() == '{')
                    {
                        layer++;
                    }
                    else if (i_sr.Peek() == '}')
                    {
                        layer--;
                    }                        
                    i_sr.Read();
                }
                while (layer > 0 && i_sr.Peek() != -1);
                if (i_sr.Peek() == '}')
                {
                    i_sr.Read();
                }                 
            }
            else
            {
                char ch;
                while (i_sr.Peek() != -1 && !Reader.IsEmptySpace(ch = (char)i_sr.Peek()) && ch != '#')
                {
                    i_sr.Read();
                }
            }
            return i_sr.Peek() != -1;
        }
    }
}
