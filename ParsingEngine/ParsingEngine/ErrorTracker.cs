using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsingEngine
{
    public class ErrorTracker
    {
        //Singleton pattern to access error checker anywhere
        private static ErrorTracker instance = null;

        public static ErrorTracker Instance 
        { 
            get 
            { 
                if (instance == null)
                {
                    instance = new ErrorTracker();
                }
                return instance; 
            } 
        }

        List<string> errors = new List<string>();

        public void ThrowError(string message)
        {
            errors.Add(message);
            Console.WriteLine(message);
        }


        public void ExportErrors(string path)
        {
            // Spit out log of all errors that accured in the program
            using (FileStream fs = File.Create(path + "\\Errors.txt"))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    if (errors.Count > 0)
                    {
                        foreach (var error in errors)
                        {
                            sw.WriteLine($"Error: {error}");
                        }
                    }
                    else
                    {
                        sw.WriteLine("Program executed succesfully with no errors.");
                    }
                }
            }
            
        }
    }
}
