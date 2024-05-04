using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.Metadata;
using ZMHDotNetCore.RestAPI.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using ZMHDotNetCore.Shared;

namespace ZMHDotNetCore.RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogADODotNet2Controller : ControllerBase
    {
        private readonly AdoDotNetServices _adoDotNetServices = new AdoDotNetServices(connectionStrings.stringBuilder.ConnectionString);

        [HttpGet]
        public IActionResult getBlogs()
        {
            string query = "SELECT * FROM tbl_blog";
            var list = _adoDotNetServices.Query<blogModel>(query);

            return Ok(list);
        }

        [HttpGet("{id}")]
        public IActionResult getBlog(int id)
        {
            string query = "SELECT * FROM tbl_blog WHERE BlogId = @BLogId";
            var blog = _adoDotNetServices.QueryFirstOrDefault<blogModel>(query, new AdoDotNetParameters("@BlogId", id));
            if(blog is null)
            {
                return NotFound("no data found");
            }
            return Ok(blog);
        }

        [HttpPost]
        public IActionResult createBlog(blogModel blog)
        {
            string query = @"INSERT INTO [dbo].[Tbl_Blog]
                           ([BlogTitle]
                           ,[BlogAuthor]
                           ,[BlogContent])
                     VALUES
                            (@BlogTitle,
                             @BlogAuthor,
                             @BlogContent)";
            var result = _adoDotNetServices.Execute(query,
                new AdoDotNetParameters("@BlogTitle", blog.BlogTitle),
                new AdoDotNetParameters("@BlogAuthor", blog.BlogAuthor),
                new AdoDotNetParameters("@BlogContent", blog.BlogContent)
                );

            string msg = result > 0 ? "created success" : "failde";
            return Ok(msg);
        }

        [HttpPut("{id}")]
        public IActionResult updateBlog(int id, blogModel blog)
        {
            SqlConnection connection = new SqlConnection(connectionStrings.stringBuilder.ConnectionString);
            connection.Open();

            string findQuery = "SELECT * FROM tbl_blog WHERE BlogId = @BlogId";
            SqlCommand findCmd = new SqlCommand(findQuery, connection);
            findCmd.Parameters.AddWithValue("@BlogId", id);
            SqlDataAdapter adapter = new SqlDataAdapter(findCmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            connection.Close();

            if (dt.Rows.Count == 0) return NotFound("no data to update");

            string updateQuery = @"UPDATE [dbo].[Tbl_Blog]
                           SET [BlogTitle] = @BlogTitle
                              ,[BlogAuthor] = @BlogAuthor
                              ,[BlogContent] = @BlogContent
                         WHERE BlogId = @BlogId";
            var result = _adoDotNetServices.Execute(updateQuery,
                new AdoDotNetParameters("@BlogTitle", blog.BlogTitle),
                new AdoDotNetParameters("@BlogAuthor", blog.BlogAuthor),
                new AdoDotNetParameters("@BlogContent", blog.BlogContent)

                );

            string msg = result > 0 ? "updated success" : "failed";
            return Ok(msg);
        }

        [HttpPatch("{id}")]
        public IActionResult patchBlog(int id, blogModel blog) 
        {
            SqlConnection connection = new SqlConnection(connectionStrings.stringBuilder.ConnectionString);
            connection.Open();

            string findQuery = "SELECT * FROM tbl_blog WHERE BlogId = @BlogId";
            SqlCommand findCmd = new SqlCommand(findQuery, connection);
            findCmd.Parameters.AddWithValue("@BlogId", id);
            SqlDataAdapter adapter = new SqlDataAdapter(findCmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            if (dt.Rows.Count == 0) return NotFound("no data to update");

            string condition = string.Empty;

            if (!string.IsNullOrEmpty(blog.BlogTitle))
            {
                condition += " [BlogTitle] = @BlogTitle, ";
            }
            if (!string.IsNullOrEmpty(blog.BlogContent))
            {
                condition += " [BlogContent] = @BlogContent, ";
            }
            if (!string.IsNullOrEmpty(blog.BlogAuthor))
            {
                condition += " [BlogAuthor] = @BlogAuthor, ";
            }

            if (condition.Length == 0)
            {
                return NotFound("No data found!");
            }

           // condition = condition.Substring(0, condition.Length - 1);
            condition = condition.TrimEnd(',', ' '); 

            string patchQuery = $@"UPDATE [dbo].[Tbl_Blog]
                           SET {condition}
                         WHERE BlogId = @BlogId";
            //blog.BlogId = id; 


            SqlCommand cmd = new SqlCommand(patchQuery, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            if (!string.IsNullOrEmpty(blog.BlogTitle))
            {
                cmd.Parameters.AddWithValue("@BlogTitle", blog.BlogTitle);
            }
            if (!string.IsNullOrEmpty(blog.BlogContent))
            {
                cmd.Parameters.AddWithValue("@BlogContent", blog.BlogContent);
            }
            if (!string.IsNullOrEmpty(blog.BlogAuthor))
            {
                cmd.Parameters.AddWithValue("@BlogAuthor", blog.BlogAuthor);
            }
            int result = cmd.ExecuteNonQuery();

            connection.Close();

            string msg = result > 0 ? "patched success" : "failed";
            return Ok(msg);
        }


        [HttpDelete("{id}")]
        public IActionResult deleteBlog(int id)
        {
            string query = @"DELETE FROM [dbo].[Tbl_Blog] WHERE BlogId = @BlogId";
            int result = _adoDotNetServices.Execute(query, new AdoDotNetParameters("@BlogId", id));

            string msg = result > 0 ? "deleted success" : "failed";
            return Ok(msg);
        }
    }
}
