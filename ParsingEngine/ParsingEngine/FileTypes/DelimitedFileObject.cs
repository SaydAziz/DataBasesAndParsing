using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace ParsingEngine.FileTypes
{
    public class DelimitedFileObject : IParsable
    {
        public DelimitedFileObject(string path)
        {
            Path = path;
            Type = CheckType();
        }

        public string Path { get; set; }

        public FileType Type { get; set; }

        public char Delimiter { get; private set; }

        // Method to check the file extension and determine the FileType and delimiter.
        private FileType CheckType()
        {
            string extension = Path.Substring(Path.LastIndexOf('.') + 1);
            switch (extension)
            {
                case "csv":
                    Delimiter = ',';
                    return FileType.CSV;
                case "txt":
                    Delimiter = '|';
                    return Type = FileType.Pipe;
                default:
                    ErrorTracker.Instance.ThrowError($"This file type is not supported by the Delimited Parser.");
                    return Type = FileType.NA;
            }
        }

        public override string ToString()
        {
            return Path;
        }
    }
}
