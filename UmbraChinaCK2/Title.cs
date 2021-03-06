﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmbraChinaCK2
{
    enum TitleType
    {
        None,
        Baron,
        Count,
        Duke,
        King,
        Emporer
    }

    class Title
    {
        static Dictionary<char, TitleType> charToTitleType = new Dictionary<char, TitleType> { { 'b', TitleType.Baron }, { 'c', TitleType.Count }, { 'd', TitleType.Duke }, { 'k', TitleType.King }, { 'e', TitleType.Emporer } };
        public List<Title> vassals = new List<Title>();
        public Title liege;
        public SortedList<DateTime, Person> history = new SortedList<DateTime, Person>();
        public SortedList<DateTime, string> DateTimes = new SortedList<DateTime, string>();
        public SortedList<DateTime, string> lieges = new SortedList<DateTime, string>();
        public SortedList<DateTime, DateTime> Intervals = new SortedList<DateTime, DateTime>();
        public string name;
        public bool fictional;
        public bool viceroy;
        public Title captial
        {
            get {
                if (vassals != null && vassals.Count > 0)
                {
                    return vassals[0];
                }
                return null;
            }
        }

        public TitleType TitleType
        {
            get
            {
                if (name != null && name.Length > 0)
                {
                    return charToTitleType[name[0]];
                }
                return TitleType.None;
            }
        }


        public static bool IsTitle(string title)
        {
            if (title.Length < 3)
            {
                return false;
            }
            if (title[1] != '_')
            {
                return false;
            }
            return (title[0] == 'b' || title[0] == 'c' || title[0] == 'd' || title[0] == 'k' || title[0] == 'e');
        }

        public static void BuildVassal(Title i_liege, Title i_vassal)
        {
            Debug.Assert(i_liege != null, "liege is null");
            Debug.Assert(i_vassal != null, "vassal is null");
            if (i_liege != null && i_vassal != null)
            {
                i_liege.vassals.Add(i_vassal);
                i_vassal.liege = i_liege;
            }
        }

        public static Title GetRootTitleString(string i_title)
        {            
            Title temp = GetTitleFromString(i_title);
            while(temp != null && temp.liege != null)
            {
                temp = temp.liege;
            }
            return temp;
        }

        public static Title GetTitleFromString(string i_title)
        {
            foreach (Title title in China.titles)
            {
                if (title.name == i_title)
                {
                    return title;
                }
            }
            return null;
        }
    }
}
