//Parser was taken from ParserEngine assignment
//Replace query was inspired from Stack Overflow
//GetDate was found on stack overflow
//Rounding for float and Date parse was found on stack overflow

using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using ProduceDBUpdater;

string path = Environment.CurrentDirectory + @"\TextFiles";
string[] filePaths;
List<List<string>> Produce;
List<string> newProduce = new List<string>();

// Get an array of file paths in the specified folder.
filePaths = Directory.GetFiles(path);
Produce = Parser.ReadFile(filePaths[0]);


SqlConnectionStringBuilder sqlConBld = new SqlConnectionStringBuilder();
sqlConBld["server"] = @"(localdb)\MSSQLLocalDB";
sqlConBld["Trusted_Connection"] = true;
sqlConBld["Integrated Security"] = "SSPI";
sqlConBld["Initial Catalog"] = "PROG260FA23";
string sqlConStr = sqlConBld.ToString();



using (SqlConnection conn = new SqlConnection(sqlConStr))
{
    conn.Open();
    foreach ( var item in Produce)
    {
        //Populating database with values read from file
        string insertCommand = $@"INSERT [dbo].[Produce] ([Name], [Location], [Price], [UoM], [SellByDate]) VALUES ('{item[0]}', '{item[1]}', '{float.Parse(item[2])}', '{item[3]}', '{item[4]}')";
        using (var command = new SqlCommand(insertCommand, conn))
        {
            var query = command.ExecuteNonQuery();
        }
    }
    
    string locSwitchCommand = $@"UPDATE Produce SET Location = REPLACE(Location, 'F', 'Z') WHERE Location LIKE '%f%';";
    using (var command = new SqlCommand(locSwitchCommand, conn))
    {
        var query = command.ExecuteNonQuery();
    }

    string delExpiredCommand = $@"DELETE FROM Produce WHERE ([SellByDate] < GETDATE());";
    using (var command = new SqlCommand(delExpiredCommand, conn))
    {
        var query = command.ExecuteNonQuery();
    }

    string addPriceCommand = $@"UPDATE Produce SET Price = Price + 1";
    using (var command = new SqlCommand(addPriceCommand, conn))
    {
        var query = command.ExecuteNonQuery();
    }
    conn.Close();
}

//Reading database values into string list to print to file
using (SqlConnection conn = new SqlConnection(sqlConStr))
{
    conn.Open();
    string inLineSQL = @"Select * from Produce";
    using (var command = new SqlCommand(inLineSQL, conn))
    {
        var reader = command.ExecuteReader();

        while (reader.Read())
        {
            string item = $"{reader.GetValue(1)}|{reader.GetValue(2)}|{reader.GetValue(3)}|{reader.GetValue(4)}|{reader.GetValue(5)}";
            newProduce.Add(item);
        }
        reader.Close();
    }
    conn.Close();
}

Parser.WriteFile(filePaths[0], newProduce);
