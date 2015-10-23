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
            string basePath = "C:/Users/u0950711/Documents/Umbra-Spherae-versions";
            LoadTitles.LoadTitlesFromFile(Path.Combine(basePath, "Umbra Spherae/common/landed_titles/US_China.txt"));
            LoadHistories.LoadHistoriesFromFolder(Path.Combine(basePath, "Umbra Spherae/history/titles"));
            LoadDynasties.LoadDynastiesFromFile(Path.Combine(basePath, "Umbra Spherae/common/dynasties/china_pinyin.txt"));
            Process.CalculateInterval();
            GenerateDynasties.GenCount();
            GenerateDynasties.GenDuke();
            GenerateDynasties.GenKing();
            WriteOut.OutputTitles("C:/Users/u0950711/Documents/UmbraChinaCK2/Output/titles");
            WriteOut.OutputDynasties("C:/Users/u0950711/Documents/UmbraChinaCK2/Output/dynasties/china_autogen.txt");
            WriteOut.OutputPeople("C:/Users/u0950711/Documents/UmbraChinaCK2/Output/characters/han_autogen.txt");
        }
    }
}
