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
                    values = currentLine.Split('|').ToList();
                    Items.Add(values);
                    currentLine = sr.ReadLine();
                }
            }


            return Items;

        }

        // WriteFile method writes the parsed data to a new text file.
        public static void WriteFile(string path, List<string> items)
        {
            List<string> newItems = new List<string>();
            // Create a new file name based on the original file name.
            string newFileName = path.Substring(path.LastIndexOf('\\') + 1);
            newFileName = newFileName.Insert(newFileName.LastIndexOf('.'), "_out");
            newFileName = newFileName.Replace(newFileName.Substring(newFileName.LastIndexOf('.')), ".txt");

            // Construct the new file path.
            string newPath = path.Substring(0, path.LastIndexOf('\\') + 1) + "\\" + newFileName;

            // Use FileStream and StreamWriter to create and write to the new file.
            using (FileStream fs = File.Create(newPath))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.WriteLine("Name,Location,Price,UoM,Sell_by_Date");
                    foreach (string item in items)
                    {
                        //splitting string to reformat string values
                        string[] newVals = item.Split('|');

                        float price = float.Parse(newVals[2]);
                        newVals[2] = price.ToString("0.00");

                        newVals[4] = DateTime.Parse(newVals[4]).ToString("MM-dd-yyyy");
                        //reconstructing string to print
                        newItems.Add($"{newVals[0]}|{newVals[1]}|{newVals[2]}|{newVals[3]}|{newVals[4]}");
                    }
                    
                    foreach(string item in newItems)
                    {
                        sw.WriteLine(item);
                    }
                }
            }
        }
    }
}
