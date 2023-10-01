//File scraper was taken from my original IO assignment

using ParsingEngine;
using System.IO;
using System.Runtime.CompilerServices;

    string[] filePaths;
    List<IParsable> toParse = new List<IParsable>();
    string path = Environment.CurrentDirectory + @"\TextFiles";
    if (!Directory.Exists(path))
    {
        Console.WriteLine("Folder does not exist.");
    }
    else
    {
        filePaths = Directory.GetFiles(path);

        foreach (string filePath in filePaths)
        {
            toParse.Add(new TextFileObject(filePath));
        }

        ParserEngine.StartParsing(toParse);
        //Console.WriteLine("\nFiles in folder: \n");
        //foreach (var file in toParse)
        //{
        //    Console.WriteLine(file.Type + " " + file.ToString());
        //    Console.WriteLine(" ");
        //}
    }


    Console.ReadKey(true);