using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsingEngine
{
    public enum FileType
    {
        CSV,
        Pipe,
        Json,
        XML,
        NA
    }

    public interface IParsable
    {
        public string Path { get; set; }

        public FileType Type { get; set; }
    }
}
