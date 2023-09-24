//Linked list implementation was helped by Gen Grevious on youtube
using FileIO_Monster;
using System.Text;
using System.Threading;

List<Entity> entities = new List<Entity>();

using (StreamReader sr = new StreamReader("Stats.txt"))
{
    List<string> lines;
    string header = sr.ReadLine();
    string? currentLine = sr.ReadLine();

    while (currentLine != null)
    {
        
        string[] charStats = currentLine.Split(' ');
        entities.Add(new Entity(charStats[0], Int32.Parse(charStats[1]), Int32.Parse(charStats[2]), Int32.Parse(charStats[3]), Int32.Parse(charStats[4])));
        currentLine = sr.ReadLine();
    }  
}

Game gameInstance = new Game(entities);
string result = gameInstance.StartGame();

using (FileStream fs = File.Create(@"E:\Coding Projects\FileIO Monster\FileIO Monster\Results.txt"))
{
    using (StreamWriter sw = new StreamWriter(fs))
    {
        sw.WriteLine(result);
    }
}
    
Console.ReadKey(true);
