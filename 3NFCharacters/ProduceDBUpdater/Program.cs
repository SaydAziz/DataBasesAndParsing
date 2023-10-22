//Parser was taken from ParserEngine assignment
//Replace query was inspired from Stack Overflow
//GetDate was found on stack overflow
//Rounding for float and Date parse was found on stack overflow

using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using ProduceDBUpdater;
using System.Runtime.CompilerServices;

string path = Environment.CurrentDirectory + @"\TextFiles";
string[] filePaths;
List<List<string>> Characters;
List<string> join = new List<string>();

// Get an array of file paths in the specified folder.
filePaths = Directory.GetFiles(path);
Characters = Parser.ReadFile(filePaths[0]);


SqlConnectionStringBuilder sqlConBld = new SqlConnectionStringBuilder();
sqlConBld["server"] = @"(localdb)\MSSQLLocalDB";
sqlConBld["Trusted_Connection"] = true;
sqlConBld["Integrated Security"] = "SSPI";
sqlConBld["Initial Catalog"] = "PROG260FA23";
string sqlConStr = sqlConBld.ToString();



using (SqlConnection conn = new SqlConnection(sqlConStr))
{
    conn.Open();
    foreach (var item in Characters)
    {
        string? currentLoc = null;
        //Checking if Location already exists
        string checkLocCommand = $@"SELECT * FROM Locations WHERE LocationName LIKE '{item[2]}'";
        using (var command = new SqlCommand(checkLocCommand, conn))
        {
            var query = command.ExecuteNonQuery();
            var reader = command.ExecuteReader();

            if (reader.Read())
            {
                currentLoc = $"{reader.GetValue(0)}";
            }
            reader.Close();
        }
        //Populating Location database with location if its not already in there
        if (currentLoc == null)
        {
            string insert2Command = $@"INSERT [dbo].[Locations] ([LocationName]) VALUES ('{item[2]}')";
            using (var command = new SqlCommand(insert2Command, conn))
            {
                var query = command.ExecuteNonQuery();
            }
        }
        int currentLocID;
        //Get ID of LocationName
        string getLocIDCommand = $@"SELECT TOP 1 ID FROM Locations WHERE LocationName LIKE '{item[2]}'";
        using (var command = new SqlCommand(getLocIDCommand, conn))
        {
            var query = command.ExecuteNonQuery();
            var reader = command.ExecuteReader();

            reader.Read();
            currentLocID = int.Parse($"{reader.GetValue(0)}");
            reader.Close();
        }
        int charID;
        //Populating Characters database with values read from file + ID of location from its own table
        string insertCommand = $@"INSERT [dbo].[Characters] ([Character], [Type], [Map_Location]) VALUES ('{item[0]}', '{item[1]}', '{currentLocID}')";
        using (var command = new SqlCommand(insertCommand, conn))
        {
            var query = command.ExecuteNonQuery();
        }
        //Get ID of most recent row
        string getIDCommand = $@"SELECT TOP 1 ID FROM Characters ORDER BY ID DESC";
        using (var command = new SqlCommand(getIDCommand, conn))
        {
            var query = command.ExecuteNonQuery();
            var reader = command.ExecuteReader();

            reader.Read();
            charID = int.Parse($"{reader.GetValue(0)}");
            reader.Close();
        }
        //Populating CharInfo database with values read from file
        string insert1Command = $@"INSERT [dbo].[CharInfo] ([CharID], [IsOC], [IsSwordFighter], [IsMagical]) VALUES ('{charID}', '{item[3]}', '{item[4]}', '{item[5]}')";
        using (var command = new SqlCommand(insert1Command, conn))
        {
            var query = command.ExecuteNonQuery();
        }
    }
        string locSwitchCommand = $@"UPDATE Locations SET LocationName = REPLACE(LocationName, '', NULL) WHERE LocationName LIKE '' UPDATE Characters SET Type = REPLACE(Type, '', NULL) WHERE Type LIKE ''";
        using (var command = new SqlCommand(locSwitchCommand, conn))
        {
            var query = command.ExecuteNonQuery();
        }

        int nullLoc;
        //Get ID of LocationName
        string getLocNULLCommand = $@"SELECT TOP 1 ID FROM Locations WHERE LocationName IS NULL";
        using (var command = new SqlCommand(getLocNULLCommand, conn))
        {
            var query = command.ExecuteNonQuery();
            var reader = command.ExecuteReader();

            reader.Read();
            nullLoc = int.Parse($"{reader.GetValue(0)}");
            reader.Close();
        }

        //Change all location null ids to actual null
        string updateLocNullCommand = $@"UPDATE Characters SET Map_Location = REPLACE(Map_Location, {nullLoc}, NULL) WHERE Map_Location LIKE {nullLoc}";
        using (var command = new SqlCommand(updateLocNullCommand, conn))
        {
            var query = command.ExecuteNonQuery();
        }

        string delLocNull = $@"DELETE FROM Locations WHERE LocationName IS NULL";
        using (var command = new SqlCommand(delLocNull, conn))
        {
            var query = command.ExecuteNonQuery();
        }


        conn.Close();
    
}

//Reading database inner join values into string list to print to file
using (SqlConnection conn = new SqlConnection(sqlConStr))
{
    conn.Open();
    string inLineSQL = @"SELECT * FROM  Characters INNER JOIN CharInfo ON Characters.ID = CharInfo.CharID INNER JOIN Locations ON Characters.Map_Location = Locations.ID";
    using (var command = new SqlCommand(inLineSQL, conn))
    {
        var reader = command.ExecuteReader();

        while (reader.Read())
        {
            string item = $"{reader.GetValue(0)},{reader.GetValue(1)},{reader.GetValue(2)},{reader.GetValue(3)},{reader.GetValue(4)},{reader.GetValue(5)},{reader.GetValue(6)},{reader.GetValue(7)},{reader.GetValue(8)},{reader.GetValue(9)},{reader.GetValue(10)}";
            join.Add(item);
        }
        reader.Close();
    }
    conn.Close();
}

Parser.WriteFile(path, "\\Full Report.txt", join, "ID,Character,Type,Map_Location,CharInfoID,CharID,IsOriginalCharacter,IsSwordFighter,IsMagical,LocationID,LocationName");
join.Clear();

//Reading database Left Join values into string list to print to file
using (SqlConnection conn = new SqlConnection(sqlConStr))
{
    conn.Open();
    string inLineSQL = @"SELECT * FROM  Characters LEFT JOIN Locations ON Characters.Map_Location = Locations.ID WHERE Characters.Map_Location IS NULL";
    using (var command = new SqlCommand(inLineSQL, conn))
    { 
        var reader = command.ExecuteReader();

        while (reader.Read())
        {
            string item = $"{reader.GetValue(1)}";
            join.Add(item);
        }
        reader.Close();
    }
    conn.Close();
}

Parser.WriteFile(path, "\\Lost.txt", join, "Characters with no maps:");

join.Clear();
//Reading database Left Join values into string list to print to file
using (SqlConnection conn = new SqlConnection(sqlConStr))
{
    conn.Open();
    string inLineSQL = @"SELECT * FROM  Characters LEFT JOIN CharInfo ON Characters.ID = CharInfo.CharID WHERE IsSwordFighter = 1 AND NOT Type = 'Human'";
    using (var command = new SqlCommand(inLineSQL, conn))
    {
        var reader = command.ExecuteReader();

        while (reader.Read())
        {
            string item = $"{reader.GetValue(1)}";
            join.Add(item);
        }
        reader.Close();
    }
    conn.Close();
}
Parser.WriteFile(path, "\\SwordNonHuman.txt", join, "Non-Human characters who can fight with a sword:");


