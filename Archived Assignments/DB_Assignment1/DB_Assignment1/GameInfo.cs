//list to dictionary method was found on stack overflow
//!charwhitespace is take from stack overflow
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DB_Assignment1
{
    public class GameInfo
    {
        public GameInfo()
        {
            MetaData = new List<Info>();
            Info info1 = new Info();
            info1.Id = 0;
            info1.Name = "Monkey Island";
            info1.Genre = "Point & Click";
            info1.MapNames = new string[] {"Guybrush Mansion","LeChuck Hideout","Melee Island","SCUMM Bar" };
            MetaData.Add(info1);
            Info info2 = new Info();
            info2.Id = 1;
            info2.Name = "Mario Odyssey";
            info2.Genre = "Adventure";
            info2.MapNames = new string[] { "Mushroom Kingdom", "Cap Kingdom", "Cloud Kingdom", "Snow Kingdom" };
            MetaData.Add(info2);
            Info info3 = new Info();
            info3.Id = 2;
            info3.Name = "Final Fantasy 10";
            info3.Genre = "Adventure";
            info3.MapNames = new string[] { "Besaid Island", "Bevelle", "Calm Lands", "Baaj Temple" };
            MetaData.Add(info3);
            Info info4 = new Info();
            info4.Id = 3;
            info4.Name = "Valkyra 4";
            info4.Genre = "Tactical RPG";
            info4.MapNames = new string[] { "Battle of Siegval", "Other Kai", "Azure Flame", "Midnight Run" };
            MetaData.Add(info4);

            GameMetaData = MetaData.ToDictionary(item => item.Id, item => item);
            GenreCount = new Dictionary<string, int>();

            GenreCount.Add("Tactical RPG", 0);
            GenreCount.Add("Adventure", 0);
            GenreCount.Add("Point & Click", 0);
            
        }
        public List<Info> MetaData { get; set; }
        public Dictionary<int, Info> GameMetaData { get; set; }
        public Dictionary<string, int> GenreCount { get; set; }


        public void PrintData()
        {
            foreach (var item in  GameMetaData)
            {
                string mapNames = string.Join(", ", item.Value.MapNames);
                Console.WriteLine("ID: " + item.Key.ToString() + "\nName: " + item.Value.Name + "\nGenre: " + item.Value.Genre + "\nMaps: " + mapNames + "\n");
            }
        }

        public void CheckGenre()
        {
            string mostGenre = null;
            foreach(var item in MetaData)
            {
                GenreCount[item.Genre] += 1;
            }

            foreach(var item in GenreCount)
            {
                if (mostGenre == null || item.Value > GenreCount[mostGenre])
                {
                    mostGenre = item.Key;
                }
            }
           
            Console.WriteLine("Most present genre: " +  mostGenre + "\n");
        }

        public void CheckMap()
        {
            string[] longestNames = { "(", "(", "(" };
            List<string> zNames = new List<string>();


            foreach(Info info in MetaData)
            {
                foreach(string name in info.MapNames)
                {
                    if (name.Contains('z'))
                    {
                        zNames.Add(name);
                    }

                    for (int i = 0; i < longestNames.Length; i++)
                    {
                        if (name.Count(x => !Char.IsWhiteSpace(x)) > longestNames[i].Substring(0, longestNames[i].IndexOf("(")).Count(x => !Char.IsWhiteSpace(x)))
                        {
                            longestNames[i] = name + " (" + info.Name + ")";
                            break;
                        }
                    }
                }
            }

            Console.WriteLine("Top 3 Longest Map Names:");

            foreach (string name in longestNames)
            {
                Console.Write(name +   "\n");
            }
            Console.WriteLine("\n");

            Console.WriteLine("Map names with z in them:");

            foreach (string name in zNames)
            {
                Console.Write(name + "\n");
            }
            Console.WriteLine("\n");
        }
    }
}