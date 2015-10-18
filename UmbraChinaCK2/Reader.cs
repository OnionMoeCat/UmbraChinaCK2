using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmbraChinaCK2
{
    class Reader
    {
        static public bool IsEmptySpace(char i_char)
        {
            return (i_char == ' ' || i_char == '\t' || i_char == '\r' || i_char == '\n');
        }

        static public bool IsToken(char i_char)
        {
            return (i_char == '=' || i_char == '{' || i_char == '}' || i_char == '"');
        }

        static public bool ReadAToken(StringReader i_sr, char i_token)
        {
            if (i_sr.Peek() == i_token)
            {
                i_sr.Read();
                return true;
            }
            return false;
        }
        static public bool ReadAKey(StringReader i_sr, out string key)
        {
            key = "";            
            char ch = (char)i_sr.Peek();
            while (!IsEmptySpace(ch) && !IsToken(ch))
            {
                key += (char)i_sr.Read();
                ch = (char)i_sr.Peek();                
            }
            return (key.Length > 0);
        }
        static public bool ReadATime(StringReader i_sr, out Time time)
        {
            time = new Time();
            string key = "";
            char ch = (char)i_sr.Peek();
            while (!IsEmptySpace(ch) && !IsToken(ch))
            {
                key += (char)i_sr.Read();
                ch = (char)i_sr.Peek();
            }
            string[] nums = key.Split('.');
            if (nums.Length != 3)
            {
                return false;
            }
            try
            {
                time.year = Int32.Parse(nums[0]);
                time.month = Int32.Parse(nums[1]);
                time.day = Int32.Parse(nums[2]);
            }
            catch (FormatException)
            {
                Debug.Assert(false, "Bad Time Format");
            }
            catch (OverflowException)
            {
                Debug.Assert(false, "Bad Time Format");
            }
            return true;
        }
        static public bool ReadSpaces(StringReader i_sr)
        {
            char ch = (char)i_sr.Peek();
            while (IsEmptySpace(ch) || ch == '#')
            {
                if (ch == '#')
                {
                    ReadToEndOfLine(i_sr);
                }
                else
                {
                    i_sr.Read();
                }
                ch = (char)i_sr.Peek();
            }
            return true;
        }
        static public bool ReadToEndOfLine(StringReader i_sr)
        {
            char ch = (char)i_sr.Peek();
            while (ch != '\n')
            {                
                i_sr.Read();
                ch = (char)i_sr.Peek();
            }
            return true;
        }

        static public bool ReadAString(StringReader i_sr, out string key)
        {
            key = "";
            if (!ReadAToken(i_sr, '"'))
            {
                Debug.Assert(false, "A Token '\"' Expected");
                return false;
            }
            char ch = (char)i_sr.Peek();
            while (!IsToken(ch))
            {
                key += (char)i_sr.Read();
                ch = (char)i_sr.Peek();
            }
            if (!ReadAToken(i_sr, '"'))
            {
                Debug.Assert(false, "A Token '\"' Expected");
                return false;
            }
            return (key.Length > 0);
        }

        static public bool ReadValue(StringReader i_sr)
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
