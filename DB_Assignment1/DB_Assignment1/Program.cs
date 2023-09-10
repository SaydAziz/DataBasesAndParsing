using DB_Assignment1;


GameInfo games = new GameInfo();

Console.WriteLine("Number of Games: " + games.MetaData.Count() + "\n");
games.CheckGenre();
games.CheckMap();

games.PrintData();

Console.ReadKey();


