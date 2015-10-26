using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmbraChinaCK2
{
    class Dynasty
    {
        public int id;
        public string name;
        public string culture;
    }
    class Name
    {
        public string first;
        public string middle;
        public Dynasty dynasty;
        const int NUM = 50;
        static string[] names = new string[NUM]{ 
            "An", "Shi", "Guo", "Jing", "Gang", 
            "Xian", "Xiao", "Ping", "Zhong", "Yi",
            "Zhong", "Bo", "Zi", "Da", "Hua",
            "Su", "Qing", "Guang", "Cheng", "Pu",
            "Jing", "Chong", "Cai", "Ren", "Xin",
            "Xi", "Hao", "Gong", "Quan", "Wan",
            "Li", "Shu", "Xiu", "Shi", "Zhe",
            "Xun", "Gong", "Xiang", "Wen", "Bi",
            "Zheng", "Hong", "Fei", "Hui", "Dao",
            "Song", "Yuan", "Min", "Long", "Ji"
            };
        public static string RandomName()
        {
            return names[Rnd.rnd.Next(0, NUM)];
        }
    }
    class Person
    {
        public int id;
        public Person father;
        public List<Person> children = new List<Person>();
        public DateTime born;
        public DateTime die;
        public Name name;
        public string religion;
        public Dynasty dynasty
        {
            get
            {
                return name.dynasty;
            }
        }
    }
}
