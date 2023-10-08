using ParsingEngine;
using System.IO;
using System.Runtime.CompilerServices;

// Define an array to store file paths and a list to store IParsable objects.
string[] filePaths;
List<IParsable> toParse = new List<IParsable>();

string path = Environment.CurrentDirectory + @"\TextFiles";

if (!Directory.Exists(path))
{
    Console.WriteLine("Folder does not exist.");
}
else
{
    // Get an array of file paths in the specified folder.
    filePaths = Directory.GetFiles(path);

    // Iterate through the file paths and create TextFileObject instances for parsing.
    foreach (string filePath in filePaths)
    {
        toParse.Add(new TextFileObject(filePath));
    }

    // Start the parsing process using the ParserEngine class.
    ParserEngine.StartParsing(toParse);
}

