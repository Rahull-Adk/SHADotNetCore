using System.Data.SqlClient;
using System.Data;

const string connectionString =
    "Data Source=RAHULL;Initial Catalog=DotnetTrainingBatch5;User ID=sa;Password=Rahulltheprogrammer06!;";
SqlConnection connection = new SqlConnection(connectionString);
Console.WriteLine(connectionString + ": " + connectionString);
Console.WriteLine("Connecting to database...");
connection.Open();
Console.WriteLine("Connected to SQL Server");
string query = @"SELECT [BlogId]
      ,[BlogTitle]
      ,[BlogAuthor]
      ,[BlogContent]
      ,[DeleteFlag]
  FROM [dbo].[Tb1_Blog]";
SqlCommand cmd = new SqlCommand(query);
SqlDataAdapter adapter = new SqlDataAdapter(cmd);
DataTable dt = new DataTable();
adapter.Fill(dt);
Console.WriteLine("Closing connection...");
connection.Close();
Console.WriteLine("Connection closed");