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
        //private readonly AdoDotNetServices _adoDotNetServices = new AdoDotNetServices(ConnectionStrings.StringBuilder.ConnectionString);

        private readonly AdoDotNetServices _adoDotNetServices;

        public BlogADODotNet2Controller(AdoDotNetServices adoDotNetServices)
        {
            _adoDotNetServices = adoDotNetServices;
        }

        [HttpGet]
        public IActionResult GetBlogs()
        {
            string query = "SELECT * FROM tbl_blog";
            var list = _adoDotNetServices.Query<BlogModel>(query);

            return Ok(list);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            string query = "SELECT * FROM tbl_blog WHERE BlogId = @BLogId";
            var blog = _adoDotNetServices.QueryFirstOrDefault<BlogModel>(query, new AdoDotNetParameters("@BlogId", id));
            if(blog is null)
            {
                return NotFound("no data found");
            }
            return Ok(blog);
        }

        [HttpPost]
        public IActionResult CreateBlog(BlogModel blog)
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
        public IActionResult UpdateBlog(int id, BlogModel blog)
        {
            string findQuery = "SELECT * FROM tbl_blog WHERE BlogId = @BlogId";
            var isExist = _adoDotNetServices.QueryFirstOrDefault<BlogModel>(findQuery, new AdoDotNetParameters("@BlogId", id));
            if (isExist == null) return NotFound("no data to update");

            string updateQuery = @"UPDATE [dbo].[Tbl_Blog]
                           SET [BlogTitle] = @BlogTitle
                              ,[BlogAuthor] = @BlogAuthor
                              ,[BlogContent] = @BlogContent
                         WHERE BlogId = @BlogId";
            var result = _adoDotNetServices.Execute(updateQuery,
                new AdoDotNetParameters("@BlogId", id),
                new AdoDotNetParameters("@BlogTitle", blog.BlogTitle),
                new AdoDotNetParameters("@BlogAuthor", blog.BlogAuthor),
                new AdoDotNetParameters("@BlogContent", blog.BlogContent)

                );

            string msg = result > 0 ? "updated success" : "failed";
            return Ok(msg);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, BlogModel blog) 
        {
            string findQuery = "SELECT * FROM tbl_blog WHERE BlogId = @BlogId";
            var isExist = _adoDotNetServices.QueryFirstOrDefault<BlogModel>(findQuery, new AdoDotNetParameters("@BlogId", id));
            if (isExist == null) return NotFound("no data to patch");

            string condition = string.Empty;
            List<AdoDotNetParameters> parameters = new List<AdoDotNetParameters>();
            parameters.Add(new AdoDotNetParameters("@BlogId", id));

            if (!string.IsNullOrEmpty(blog.BlogTitle))
            {
                condition += " [BlogTitle] = @BlogTitle, ";
                parameters.Add(new AdoDotNetParameters("@BlogTitle", blog.BlogTitle));
            }
            if (!string.IsNullOrEmpty(blog.BlogContent))
            {
                condition += " [BlogContent] = @BlogContent, ";
                parameters.Add(new AdoDotNetParameters("@BlogContent", blog.BlogContent));
            }
            if (!string.IsNullOrEmpty(blog.BlogAuthor))
            {
                condition += " [BlogAuthor] = @BlogAuthor, ";
                parameters.Add(new AdoDotNetParameters("@BlogAuthor", blog.BlogAuthor));
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
            
            var result = _adoDotNetServices.Execute(patchQuery, parameters.ToArray());

            string msg = result > 0 ? "patched success" : "failed";
            return Ok(msg);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            string query = @"DELETE FROM [dbo].[Tbl_Blog] WHERE BlogId = @BlogId";
            int result = _adoDotNetServices.Execute(query, new AdoDotNetParameters("@BlogId", id));

            string msg = result > 0 ? "deleted success" : "failed";
            return Ok(msg);
        }
    }
}
