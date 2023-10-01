using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace ParsingEngine
{
    public static class ParserEngine
    {
        public static void StartParsing(List<IParsable> fileCollection)
        {
            foreach (var file in fileCollection)
            {
                DoParse(file);
            }
        }

        private static void DoParse(IParsable file)
        {
            if (file is TextFileObject)
            {
                 ReadFile((TextFileObject)file);
            }
            else
            {
                Console.WriteLine("You need a different parser for this file");
            }
            
        }


        private static void ReadFile(TextFileObject currentFile)
        {
            List<string[]> Items = new List<string[]>();
            string[] values;        
            using (StreamReader sr = new StreamReader(currentFile.Path))
            {
                string? currentLine = sr.ReadLine();

                while (currentLine != null)
                {
                    values = currentLine.Split(currentFile.Delimiter);
                    Items.Add(values);
                    currentLine = sr.ReadLine();
                }
            }

            WriteFile(currentFile.Path, Items);
        }

        private static void WriteFile(string path, List<string[]> items)
        {
            string newFileName = path.Substring(path.LastIndexOf('\\') + 1);
            newFileName = newFileName.Insert(newFileName.LastIndexOf('.'), "_out");
            newFileName = newFileName.Replace(newFileName.Substring(newFileName.LastIndexOf('.')), ".txt");
            
            string newPath = path.Substring(0, path.LastIndexOf('\\') + 1) + "\\" + newFileName;

            using (FileStream fs = File.Create(newPath))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    for (int x = 0; x < items.Count; x++)
                    {
                        sw.Write($"Line#{x + 1} :");
                        for (int i = 0; i < items[x].Length; i++)
                        {
                            sw.Write($"Field#{i + 1}={items[x][i]} ");
                            if (i != items[x].Length - 1)
                            {
                                sw.Write("==> ");
                            }
                        }
                        sw.Write("\n");
                        
                    }
                    
                }
            }
        }
    }
}
