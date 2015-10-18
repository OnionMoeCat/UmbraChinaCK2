using System;
using System.Collections.Generic;
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
            return (i_char == '=' || i_char == '{' || i_char == '}');
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
                Console.WriteLine("Bad Time Format");
            }
            catch (OverflowException)
            {
                Console.WriteLine("Bad Time Format");
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
    }
}
