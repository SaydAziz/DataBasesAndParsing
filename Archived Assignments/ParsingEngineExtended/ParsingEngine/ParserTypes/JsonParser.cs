using Newtonsoft.Json;
using ParsingEngine.FileTypes;
using ParsingEngine.JsonClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml;

namespace ParsingEngine.ParserTypes
{
    internal class JsonParser : DefaultParser
    {
        //ReadFile method reads the contents of a text file and splits it into lines based on the delimiter.
        public override void ReadFile(IParsable currentFile)
        {
            List<List<string>> Items = new List<List<string>>();
            List<string> Values = new List<string>();
            Student currentStudent;
            using (StreamReader sr = new StreamReader(currentFile.Path))
            {
                currentStudent = JsonConvert.DeserializeObject<Student>(sr.ReadToEnd());
            }

            Values = currentStudent.GetValues();

            Items.Add(Values);

            // Call WriteFile to write the parsed data to a new file.
            WriteFile(currentFile.Path, Items);
        }
    }
}
