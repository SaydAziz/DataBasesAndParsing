//Select LINQ function was found on stack overflow
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProduceDBUpdater
{
    public static class Parser
    {
        // ReadFile method reads the contents of a text file and splits it into lines based on the delimiter.
        public static List<List<string>> ReadFile(string Path)
        {
            List<List<string>> Items = new List<List<string>>();
            List<string> values;

            // Use StreamReader to read the file line by line.
            using (StreamReader sr = new StreamReader(Path))
            {
                string? currentLine = sr.ReadLine();
                currentLine = sr.ReadLine();
                while (currentLine != null)
                {
                    values = currentLine.Split(',').Select(value =>
                    {
                        if (value == "TRUE")
                        {
                            return "1";
                        }
                        else if (value == "FALSE")
                        {
                            return "0";
                        }
                        else if (value.Contains("'"))
                        {
                            value = value.Replace("'", "''");
                            return value;
                        }
                        else
                            return value;
                    }).ToList();
                   
                    
                    Items.Add(values);
                    currentLine = sr.ReadLine();
                }
            }


            return Items;

        }

        // WriteFile method writes the parsed data to a new text file.
        public static void WriteFile(string path, string name, List<string> items, string header)
        {
            List<string> newItems = new List<string>();
            // Create a new file name
            string newPath = path + name;


            // Use FileStream and StreamWriter to create and write to the new file.
            using (FileStream fs = File.Create(newPath))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.WriteLine(header);
                    //foreach (string item in items)
                    //{
                    //    //splitting string to reformat string values
                    //    string[] newVals = item.Split('|');

                    //    float price = float.Parse(newVals[2]);
                    //    newVals[2] = price.ToString("0.00");

                    //    newVals[4] = DateTime.Parse(newVals[4]).ToString("MM-dd-yyyy");
                    //    //reconstructing string to print
                    //    newItems.Add($"{newVals[0]}|{newVals[1]}|{newVals[2]}|{newVals[3]}|{newVals[4]}");
                    //}
                    
                    foreach(string item in items)
                    {
                        sw.WriteLine(item);
                    }
                }
            }
        }
    }
}
