//XML document parsing method inspired from stack overflow

using ParsingEngine;
using ParsingEngine.FileTypes;
using System.IO;
using System.Runtime.CompilerServices;

// Define an array to store file paths and a list to store IParsable objects.
string[] filePaths;
List<IParsable> toParse = new List<IParsable>();

string path = Environment.CurrentDirectory + @"\TextFiles";

if (!Directory.Exists(path))
{
    ErrorTracker.Instance.ThrowError("Source folder at specified path does not exist.");
}
else
{
    // Get an array of file paths in the specified folder.
    filePaths = Directory.GetFiles(path);

    // Iterate through the file paths and create TextFileObject instances for parsing.
    foreach (string filePath in filePaths)
    {
        toParse.Add(ParserEngine.CreateFileObject(filePath));
    }

    // Start the parsing process using the ParserEngine class.
    ParserEngine.StartParsing(toParse);

    //Spit out all errors to a file
    ErrorTracker.Instance.ExportErrors(path);
}

