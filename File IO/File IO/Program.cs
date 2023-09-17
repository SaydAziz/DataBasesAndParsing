using File_IO;
using System.IO;

Console.WriteLine("Enter folder path: ");
string path = Console.ReadLine();
string[] files;
Dictionary<string, FileProperties> fileData;

if (!Directory.Exists(path))
{
    Console.WriteLine("Folder does not exist.");
}
else
{
    files = Directory.GetFiles(path);
    FileInfo fileInfo;
    fileData = files.ToDictionary(filePath => new FileInfo(filePath).Name, filePath => new FileProperties(filePath));

    Console.WriteLine("\nFiles in folder: \n");
    foreach (var file in fileData)
    {
        Console.WriteLine(file.Key);
        file.Value.ShowCharacters();
        Console.WriteLine(" ");


    }


}



Console.ReadKey(true);