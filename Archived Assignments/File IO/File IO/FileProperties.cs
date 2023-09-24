using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_IO
{
    internal class FileProperties
    {
        public FileProperties(string path)
        {
            fileInfo = new FileInfo(path);
            ExistingCharacters = new Dictionary<char, int>();
            Name = fileInfo.Name;
            FilePath = fileInfo.FullName;
            CountCharacters();
            
        }

        FileInfo fileInfo;
        public string Name { get; set; }
        public string FilePath { get; set; }
        public Dictionary<char, int> ExistingCharacters { get; set; }

        public void CountCharacters()
        {
            StreamReader sr = new StreamReader(FilePath);

            do
            {
                char character = (char)sr.Read();
                if (!Char.IsWhiteSpace(character))
                {
                    if (ExistingCharacters.ContainsKey(character))
                    {
                        ExistingCharacters[character]++;
                    }
                    else
                    {
                        ExistingCharacters.Add(character, 1);
                    }
                }
                
               
            } while (!sr.EndOfStream);
        }

        public void ShowCharacters()
        {
            foreach (var character in ExistingCharacters) 
            {
                Console.WriteLine(character.Key + ": " + character.Value);

            }
        }

    }
}
