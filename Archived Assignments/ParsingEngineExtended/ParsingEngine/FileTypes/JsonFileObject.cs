using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsingEngine.FileTypes
{
    internal class JsonFileObject : IParsable
    {
        public JsonFileObject(string path)
        {
            Path = path;
            Type = FileType.Json;
        }

        public string Path { get; set; }

        public FileType Type { get; set; }

        public override string ToString()
        {
            return Path;
        }
    }
}
