using System;
using System.Collections.Generic;
using System.IO; // Added using directive for StreamReader, FileStream, and StreamWriter
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsingEngine
{
    public static class ParserEngine
    {
        // StartParsing method iterates through a collection of IParsable files and calls DoParse on each.
        public static void StartParsing(List<IParsable> fileCollection)
        {
            foreach (var file in fileCollection)
            {
                DoParse(file);
            }
        }

        // DoParse method checks if the file is a TextFileObject and calls ReadFile if it is.
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

        // ReadFile method reads the contents of a text file and splits it into lines based on the delimiter.
        private static void ReadFile(TextFileObject currentFile)
        {
            List<string[]> Items = new List<string[]>();
            string[] values;

            // Use StreamReader to read the file line by line.
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

            // Call WriteFile to write the parsed data to a new file.
            WriteFile(currentFile.Path, Items);
        }

        // WriteFile method writes the parsed data to a new text file.
        private static void WriteFile(string path, List<string[]> items)
        {
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
