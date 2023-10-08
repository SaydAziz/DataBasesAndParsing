using ParsingEngine.FileTypes;
using ParsingEngine.ParserTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsingEngine
{
    public static class ParserEngine
    {
        public static IParsable CreateFileObject(string Path)
        {
            string extension = Path.Substring(Path.LastIndexOf('.') + 1);
            switch (extension)
            {
                case "csv":
                    return new DelimitedFileObject(Path);
                case "txt":
                    return new DelimitedFileObject(Path);
                case "json":
                    return new JsonFileObject(Path);
                case "xml":
                    return new XmlFileObject(Path);
                default:
                    ErrorTracker.Instance.ThrowError($"The file type of {Path} is not supported by the parser engine.");
                    return null;
            }
        }


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
            if (file is DelimitedFileObject)
            {
                DelimitedParser delimitedParser = new DelimitedParser();
                delimitedParser.ReadFile((DelimitedFileObject)file);
            }
            else if (file is JsonFileObject)
            {
                JsonParser jsonParser = new JsonParser();
                jsonParser.ReadFile((JsonFileObject)file);
            }
            else if (file is XmlFileObject)
            {
                XmlParser xmlParser = new XmlParser();
                xmlParser.ReadFile((XmlFileObject)file);
            }
            else
            {
                ErrorTracker.Instance.ThrowError("You need a different parser for this file");
            }
        }

        

        
    }
}
