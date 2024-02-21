using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using HCCDotNetCore.ConsoleApp.Models;

namespace HCCDotNetCore.ConsoleApp.DapperExamples
{
    public class DapperExample
    {
        private readonly SqlConnectionStringBuilder _sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = ".",
            InitialCatalog = "TestDb",
            UserID = "sa",
            Password = "sa@123"
        };
        public void Read()
        {
            string query = @"SELECT [BlogId]
                          ,[BlogTitle]
                          ,[BlogAuthor]
                          ,[BlogContent]
                      FROM [dbo].[Tbl_Blog];";

            using IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            List<BlogModel> lst = db.Query<BlogModel>(query).ToList();
            foreach (var item in lst)
            {
                Console.WriteLine(item.BlogId);
                Console.WriteLine(item.BlogTitle);
                Console.WriteLine(item.BlogAuthor);
                Console.WriteLine(item.BlogContent);
            }

        }

        public void Edit(int id)
        {
            string query = @"SELECT [BlogId]
                              ,[BlogTitle]
                              ,[BlogAuthor]
                              ,[BlogContent]
                          FROM [dbo].[Tbl_Blog] WHERE BlogId=@BlogId; ";

            BlogModel blog = new BlogModel()
            {
                BlogId = id
            };
            using IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            //BlogModel item = db.Query<BlogModel>(query, new { BlogId = id }).FirstOrDefault();
            BlogModel item = db.Query<BlogModel>(query,blog).FirstOrDefault();

            if (item is null)
            {
                Console.WriteLine("No Data Found..");
                return;
            }
            Console.WriteLine(item.BlogId);
            Console.WriteLine(item.BlogTitle);
            Console.WriteLine(item.BlogAuthor);
            Console.WriteLine(item.BlogContent);

        }

        public void Create(string title, string author, string content)
        {

            string query = @"INSERT INTO [dbo].[Tbl_Blog]
                           ([BlogTitle]
                           ,[BlogAuthor]
                           ,[BlogContent])
                     VALUES
                           (@BlogTitle
                           ,@BlogAuthor
                           ,@BlogContent); ";

            BlogModel blog = new BlogModel
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };

            using IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, blog);

            string message = result > 0 ? "Saving Successful..." : "Saving Failed...";
            Console.WriteLine(message);
        }

        public void Update(int id, string title, string author, string content)
        {

            string query = @"UPDATE [dbo].[Tbl_Blog]
                           SET [BlogTitle] = @BlogTitle
                              ,[BlogAuthor] = @BlogAuthor
                              ,[BlogContent] = @BlogContent
                         WHERE BlogId=@BlogId; ";

            BlogModel blog = new BlogModel
            {
                BlogId = id,
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };

            using IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, blog);

            string message = result > 0 ? "Update Successful..." : "Update Failed...";
            Console.WriteLine(message);
        }

        public void Delete(int id)
        {

            string query = @"DELETE FROM [dbo].[Tbl_Blog]
                             WHERE BlogId=@BlogId; ";
            BlogModel blog = new BlogModel
            {
                BlogId = id,
            };

            using IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, blog);

            string message = result > 0 ? "Delete Successful..." : "Delete Failed...";
            Console.WriteLine(message);
        }
    }
}
