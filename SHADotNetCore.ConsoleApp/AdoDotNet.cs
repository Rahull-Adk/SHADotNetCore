using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHADotNetCore.ConsoleApp
{
    public class AdoDotNet
    {
        private readonly string _connectionString =
   "Data Source=RAHULL;Initial Catalog=DotnetTrainingBatch5;User ID=sa;Password=Rahulltheprogrammer06!;";
        public void Read()
        {

            SqlConnection connection = new SqlConnection(_connectionString);
            Console.WriteLine(_connectionString + ": " + _connectionString);
            Console.WriteLine("Connecting to database...");
            connection.Open();
            Console.WriteLine("Connected to SQL Server");
            string query = @"SELECT [BlogId]
      ,[BlogTitle]
      ,[BlogAuthor]
      ,[BlogContent]
      ,[DeleteFlag]
  FROM [dbo].[Tb1_Blog] where DeleteFlag = 0";
            SqlCommand cmd = new SqlCommand(query, connection);
            //SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            //DataTable dt = new DataTable();
            //adapter.Fill(dt);

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine(reader["BlogId"]);
                Console.WriteLine(reader["BlogTitle"]);
                Console.WriteLine(reader["BlogAuthor"]);
                Console.WriteLine(reader["BlogContent"]);
                Console.WriteLine(reader["DeleteFlag"]);
            }

            Console.WriteLine("Closing connection...");
            connection.Close();
            Console.WriteLine("Connection closed");

        }

        public void Create()
        {

            Console.Write("Blog Title: ");
            string title = Console.ReadLine();
            Console.Write("Blog Author: ");
            string author = Console.ReadLine();
            Console.Write("Blog Content: ");
            string content = Console.ReadLine();


            SqlConnection connection2 = new SqlConnection(_connectionString);
            connection2.Open();
            string insertQuery = @"INSERT INTO [dbo].[Tb1_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent]
           ,[DeleteFlag])
     VALUES
           (@BlogTitle
           ,@BlogAuthor
           ,@BlogContent
           ,0)";

            SqlCommand insertCmd = new SqlCommand(insertQuery, connection2);
            insertCmd.Parameters.AddWithValue("@BlogTitle", title);
            insertCmd.Parameters.AddWithValue("@BlogAuthor", author);
            insertCmd.Parameters.AddWithValue("@BlogContent", content);
            /*
            SqlDataAdapter adapter = new SqlDataAdapter(insertCmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            */
            int result = insertCmd.ExecuteNonQuery();
            Console.WriteLine(result >= 1 ? "Successfully Saved!" : "Saving failed");
            connection2.Close();
        }

        public void Edit()
        {
            Console.Write("Blog Id: ");
            string id = Console.ReadLine();
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            string query = @"SELECT [BlogId]
      ,[BlogTitle]
      ,[BlogAuthor]
      ,[BlogContent]
      ,[DeleteFlag]
  FROM [dbo].[Tb1_Blog] WHERE BlogId = @BlogId;";
            
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("blogId", id);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            connection.Close();

            if(dt.Rows.Count == 0)
            {
                Console.WriteLine("No data found");
                return;
            }
            DataRow dr = dt.Rows[0];
            Console.WriteLine(dr["BlogId"]);
            Console.WriteLine(dr["BlogTitle"]);
            Console.WriteLine(dr["BlogAuthor"]);
            Console.WriteLine(dr["BlogContent"]);
            Console.WriteLine(dr["DeleteFlag"]);
        }

        public void Update()

        {
            Console.Write("Blog id: ");
            string id = Console.ReadLine();
            Console.Write("Blog Title: ");
            string title = Console.ReadLine();
            Console.Write("Blog Author: ");
            string author = Console.ReadLine();
            Console.Write("Blog Content: ");
            string content = Console.ReadLine();

            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
         

            string updateQuery = @"UPDATE [dbo].[Tb1_Blog]
   SET [BlogTitle] = @BlogTitle
      ,[BlogAuthor] = @BlogAuthor
      ,[BlogContent] = @BlogContent
      ,[DeleteFlag] = 0
 WHERE BlogId = @BlogId;";

            SqlCommand insertCmd = new SqlCommand(updateQuery, connection);
            insertCmd.Parameters.AddWithValue("@BlogId", id);
            insertCmd.Parameters.AddWithValue("@BlogTitle", title);
            insertCmd.Parameters.AddWithValue("@BlogAuthor", author);
            insertCmd.Parameters.AddWithValue("@BlogContent", content);

            int result = insertCmd.ExecuteNonQuery();
            Console.WriteLine(result >= 1 ? "Successfully Updated!" : "Update  failed");
            connection.Close();
        }

        public void Delete() {
            Console.Write("Enter an Id to Delete: ");
        string id = Console.ReadLine();
            SqlConnection connection = new SqlConnection( _connectionString);
            connection.Open();
            string deleteQuery = @"DELETE FROM [dbo].[Tb1_Blog]
      WHERE BlogId = @BlogId";
            SqlCommand command = new SqlCommand(deleteQuery, connection);
            command.Parameters.AddWithValue("BlogId", id);
            int result = command.ExecuteNonQuery();
            Console.WriteLine(result >= 1 ? "Successfully Deleted!" : "Delete  failed");
            connection.Close();
        }
    }

}