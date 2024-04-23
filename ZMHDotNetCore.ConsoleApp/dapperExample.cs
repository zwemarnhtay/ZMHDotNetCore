using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZMHDotNetCore.ConsoleApp
{
    internal class dapperExample
    {
        public void run()
        {
            //find(2);
            //delete(2);
            read();
            //find(1);
            //create("titleBlah", "blahAuthor", "blah blah");
            //update(1, "updated title", "unknown", "updated content");
        }

        private void read() 
        {
          using  IDbConnection db = new SqlConnection(connectionStrings.stringBuilder.ConnectionString);
          var list =  db.Query("select * from tbl_blog").ToList(); //var => List<blogDTO>

            foreach (var item in list)
            {
                Console.WriteLine(item.BlogId);
                Console.WriteLine(item.BlogTitle);
                Console.WriteLine(item.BlogAuthor);
                Console.WriteLine(item.BlogContent);
                Console.WriteLine("-------->>>");
            }
            
        }

        private void find(int id)
        {
            using IDbConnection db = new SqlConnection(connectionStrings.stringBuilder.ConnectionString);
            var item = db.Query("select * from tbl_blog where BlogId = @BlogId", new blogDTO { BlogID = id }).FirstOrDefault();

            if(item == null)
            {
                Console.WriteLine("not found!");
                return; 
            }

            Console.WriteLine(item.BlogId);
            Console.WriteLine(item.BlogTitle);
            Console.WriteLine(item.BlogAuthor);
            Console.WriteLine(item.BlogContent);
        }

        private void create(string title,  string author, string content) 
        {
            using IDbConnection db = new SqlConnection(connectionStrings.stringBuilder.ConnectionString);

            string query = @"INSERT INTO [dbo].[Tbl_Blog]
                           ([BlogTitle]
                           ,[BlogAuthor]
                           ,[BlogContent])
                     VALUES
                            (@BlogTitle,
                             @BlogAuthor,
                             @BlogContent)";

            var item = new blogDTO{BlogTitle = title, BlogAuthor = author, BlogContent = content};

            var result = db.Execute(query, item); //instead of var => int

            var msg = result > 0 ? "success" : "failed";  // var => string
            Console.WriteLine(msg);
        }

        private void update(int id, string title, string author, string content)
        {
            using IDbConnection db = new SqlConnection(connectionStrings.stringBuilder.ConnectionString);

            string query = @"UPDATE [dbo].[Tbl_Blog]
                           SET [BlogTitle] = @BlogTitle
                              ,[BlogAuthor] = @BlogAuthor
                              ,[BlogContent] = @BlogContent
                         WHERE BlogId = @BlogId";

            var item = new blogDTO { BlogID = id, BlogTitle = title, BlogAuthor = author, BlogContent = content };

            var result = db.Execute(query, item);

            var msg = result > 0 ? "success" : "failed";
            Console.WriteLine(msg);

            find(id);
        }

        private void delete(int id)
        {
            using IDbConnection db = new SqlConnection(connectionStrings.stringBuilder.ConnectionString);

            string query = @"DELETE FROM [dbo].[Tbl_Blog] WHERE BlogId = @BlogId";

            var result = db.Execute(query, new blogDTO { BlogID = id });

            var msg = result > 0 ? "success" : "failed";
            Console.WriteLine(msg);
        }
    }
}
