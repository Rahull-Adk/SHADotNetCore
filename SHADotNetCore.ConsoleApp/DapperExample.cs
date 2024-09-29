using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using SHADotNetCore.ConsoleApp.Models;
namespace SHADotNetCore.ConsoleApp
{
    public class DapperExample
    {

        private readonly string _connectionString =
 "Data Source=RAHULL;Initial Catalog=DotnetTrainingBatch5;User ID=sa;Password=Rahulltheprogrammer06!;";
        public void Read()
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            string query = "SELECT * from Tb1_Blog WHERE DeleteFlag = 0;";
            var list = db.Query<BlogDataModel>(query).ToList();

            foreach (var item in list)
            {
                Console.WriteLine(item.BlogId);
                Console.WriteLine(item.BlogTitle);
                Console.WriteLine(item.BlogAuthor);
                Console.WriteLine(item.BlogContent);
            }
        }

        public void Create(string title, string author, string content)
        {
            string query = @"INSERT INTO [dbo].[Tb1_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent]
           ,[DeleteFlag])
     VALUES
           (@BlogTitle
           ,@BlogAuthor
           ,@BlogContent
           ,0)";


            using IDbConnection db = new SqlConnection(_connectionString);
            int result = db.Execute(query, new
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            });

            Console.WriteLine(result >= 1 ? "Success" : "Failed");




        }

        public void Update(int id, string title, string author, string content)
        {
            string query = @"UPDATE [dbo].[Tb1_Blog]
   SET [BlogTitle] = @BlogTitle
      ,[BlogAuthor] = @BlogAuthor
      ,[BlogContent] = @BlogContent
      ,[DeleteFlag] = 0
 WHERE BlogId = @BlogId;";
            using IDbConnection db = new SqlConnection(_connectionString);
            int result = db.Execute(query, new { BlogId = id, BlogTitle = title, BlogAuthor = author, BlogContent = content });
            Console.WriteLine(result > 0 ? "Updated successfully" : "Update unsuccessful");
        }

        public void Delete(int id)
        {
            string query = @"DELETE FROM [dbo].[Tb1_Blog]
      WHERE BlogId = @BlogId;";
            using IDbConnection db = new SqlConnection(_connectionString);
            int result = db.Execute(query, new { BlogId = id });
            Console.WriteLine(result > 0 ? "Deleted successfully" : "Delete unsuccessful");
        }

    }

}
