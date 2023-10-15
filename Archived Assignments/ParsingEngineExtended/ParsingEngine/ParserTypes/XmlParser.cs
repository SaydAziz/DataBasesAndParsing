using ParsingEngine.FileTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ParsingEngine.ParserTypes
{
    internal class XmlParser : DefaultParser
    {
        // ReadFile method reads the contents of a text file and splits it into lines based on the delimiter.
        public override void ReadFile(IParsable currentFile)
        {
            List<List<string>> Items = new List<List<string>>();
            
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(currentFile.Path); 


            XmlNodeList itemNodes = xmlDoc.DocumentElement.SelectNodes("item");

            foreach(XmlNode node in itemNodes)
            {
                List<string> Values = new List<string>();
                foreach (XmlNode childNode in node.ChildNodes)
                {
                    Values.Add(childNode.InnerText);

                }
                Items.Add(Values);
            }         

            // Call WriteFile to write the parsed data to a new file.
            WriteFile(currentFile.Path, Items);
        }


    }
}
