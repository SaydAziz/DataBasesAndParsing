using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsingEngine.FileTypes
{
    internal class XmlFileObject : IParsable
    {
        public XmlFileObject(string path)
        {
            Path = path;
            Type = FileType.XML;
        }

        public string Path { get; set; }

        public FileType Type { get; set; }

        public override string ToString()
        {
            return Path;
        }
    }
}
