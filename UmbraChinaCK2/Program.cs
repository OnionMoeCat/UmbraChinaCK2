using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmbraChinaCK2
{
    class Program
    {
        static void Main(string[] args)
        {
            string basePath = "C:/Users/张振学/Documents/CK2Mods/Umbra-Spherae-versions";
            LoadTitles.LoadTitlesFromFile(Path.Combine(basePath, "Umbra Spherae/common/landed_titles/US_China.txt"));
            LoadHistories.LoadHistoriesFromFolder(Path.Combine(basePath, "Umbra Spherae/history/titles"));
            LoadDynasties.LoadDynastiesFromFile(Path.Combine(basePath, "Umbra Spherae/common/dynasties/china_pinyin.txt"));
            GenerateDynasties.Gen();
            GeneratePeople.GeneratePeople();
            WriteOut.WriteOut();
        }
    }
}
