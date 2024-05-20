using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.Metadata;
using ZMHDotNetCore.RestAPI.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ZMHDotNetCore.RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogADODotNetController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetBlogs()
        {
            string query = "SELECT * FROM tbl_blog";

            SqlConnection connction = new SqlConnection(ConnectionStrings.StringBuilder.ConnectionString);
            connction.Open();

            SqlCommand cmd = new SqlCommand(query, connction);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            connction.Close();

            //List<blogModel> list = new List<blogModel>();
            //foreach (DataRow dr in dt.Rows)
            //{
            //    blogModel blog = new blogModel();
            //    blog.BlogId = Convert.ToInt32(dr["BlogId"]);
            //    blog.BlogTitle = Convert.ToString(dr["BlogTitle"]);
            //    blog.BlogContent = Convert.ToString(dr["BlogContent"]);
            //    blog.BlogAuthor = Convert.ToString(dr["BlogAuthor"]);
            //    list.Add(blog);
            //}

            List<BlogModel> list = dt.AsEnumerable().Select(dr => new BlogModel
            {
                BlogId = Convert.ToInt32(dr["BlogId"]),
                BlogTitle = Convert.ToString(dr["BlogTitle"]),
                BlogContent = Convert.ToString(dr["BlogContent"]),
                BlogAuthor = Convert.ToString(dr["BlogAuthor"]),
            }).ToList();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            string query = "SELECT * FROM tbl_blog WHERE BlogId = @BLogId";

            SqlConnection connction = new SqlConnection(ConnectionStrings.StringBuilder.ConnectionString);
            connction.Open();

            SqlCommand cmd = new SqlCommand(query, connction);
            cmd.Parameters.AddWithValue("@BlogId", id);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            connction.Close();

            if(dt.Rows.Count is 0)
            {
                return NotFound("no data found");
            }

            DataRow dr = dt.Rows[0];
            var blog = new BlogModel
            {
                BlogId = Convert.ToInt32(dr["BlogId"]),
                BlogTitle = Convert.ToString(dr["BlogTitle"]),
                BlogAuthor = Convert.ToString(dr["BlogAuthor"]),
                BlogContent = Convert.ToString(dr["BlogContent"])
            };

            return Ok(blog);
        }

        [HttpPost]
        public IActionResult CreateBlog(string title, string content, string author)
        {
            string query = @"INSERT INTO [dbo].[Tbl_Blog]
                           ([BlogTitle]
                           ,[BlogAuthor]
                           ,[BlogContent])
                     VALUES
                            (@BlogTitle,
                             @BlogAuthor,
                             @BlogContent)";

            SqlConnection connection = new SqlConnection(ConnectionStrings.StringBuilder.ConnectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogTitle", title);
            cmd.Parameters.AddWithValue("@BlogContent", content);
            cmd.Parameters.AddWithValue("@BlogAuthor", author);
            int result = cmd.ExecuteNonQuery();

            connection.Close();

            string msg = result < 0 ? "created success" : "failde";
            return Ok(msg);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, BlogModel blog)
        {
            SqlConnection connection = new SqlConnection(ConnectionStrings.StringBuilder.ConnectionString);
            connection.Open();

            string findQuery = "SELECT * FROM tbl_blog WHERE BlogId = @BlogId";
            SqlCommand findCmd = new SqlCommand(findQuery, connection);
            findCmd.Parameters.AddWithValue("@BlogId", id);
            SqlDataAdapter adapter = new SqlDataAdapter(findCmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            if(dt.Rows.Count == 0) return NotFound("no data to update");

            string updateQuery = @"UPDATE [dbo].[Tbl_Blog]
                           SET [BlogTitle] = @BlogTitle
                              ,[BlogAuthor] = @BlogAuthor
                              ,[BlogContent] = @BlogContent
                         WHERE BlogId = @BlogId";

            SqlCommand cmd = new SqlCommand(updateQuery, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            cmd.Parameters.AddWithValue("@BlogTitle", blog.BlogTitle);
            cmd.Parameters.AddWithValue("@BlogAuthor", blog.BlogAuthor);
            cmd.Parameters.AddWithValue("@BlogContent", blog.BlogContent);
            int result = cmd.ExecuteNonQuery();

            connection.Close();

            string msg = result > 0 ? "updated success" : "failed";
            return Ok(msg);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, BlogModel blog) 
        {
            SqlConnection connection = new SqlConnection(ConnectionStrings.StringBuilder.ConnectionString);
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
        public IActionResult DeleteBlog(int id)
        {
            string query = @"DELETE FROM [dbo].[Tbl_Blog] WHERE BlogId = @BlogId";

            SqlConnection connection = new SqlConnection(ConnectionStrings.StringBuilder.ConnectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            int result = cmd.ExecuteNonQuery();

            connection.Close();

            string msg = result > 0 ? "deleted success" : "failed";
            return Ok(msg);
        }
    }
}
