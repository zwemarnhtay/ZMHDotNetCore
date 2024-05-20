﻿using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using ZMHDotNetCore.RestAPI.Models;

namespace ZMHDotNetCore.RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogDapperController : ControllerBase
    {
        [HttpGet]
        public IActionResult GtBlogs()
        {
            string query = "SELECT * FROM tbl_Blog";
            using IDbConnection db = new SqlConnection(ConnectionStrings.StringBuilder.ConnectionString);
            List<BlogModel> list = db.Query<BlogModel>(query).ToList();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            var item = FindData(id);
            if(item is null)
            {
                return NotFound("no data found!");
            }

            return Ok(item);
        }

        [HttpPost]
        public IActionResult CreateBlog(BlogModel Blog)
        {
            string query = @"INSERT INTO [dbo].[Tbl_Blog]
                           ([BlogTitle]
                           ,[BlogAuthor]
                           ,[BlogContent])
                     VALUES
                            (@BlogTitle,
                             @BlogAuthor,
                             @BlogContent)";
            using IDbConnection db = new SqlConnection(ConnectionStrings.StringBuilder.ConnectionString);
            int result = db.Execute(query, Blog);

            string msg = result > 0 ? "created success" : "failed";
            return Ok(msg);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, BlogModel Blog)
        {
            var item = FindData(id);
            if (item is null)
            {
                return NotFound("no data found!");
            }
            string query = @"UPDATE [dbo].[Tbl_Blog]
                           SET [BlogTitle] = @BlogTitle
                              ,[BlogAuthor] = @BlogAuthor
                              ,[BlogContent] = @BlogContent
                         WHERE BlogId = @BlogId";
            Blog.BlogId = id;
            using IDbConnection db = new SqlConnection(ConnectionStrings.StringBuilder.ConnectionString);
            var result = db.Execute(query, Blog);

            var msg = result > 0 ? "updated success" : "failed";
            return Ok(msg);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, BlogModel Blog)
        {
            var item = FindData(id);
            if (item is null)
            {
                return NotFound("no data found!");
            }

            string condition = string.Empty;

            if(!string.IsNullOrEmpty(Blog.BlogTitle))
            {
                condition += " [BlogTitle] = @BlogTitle ";
            }
            if (!string.IsNullOrEmpty(Blog.BlogAuthor))
            {
                condition += " [BlogAuthor] = @BlogAuthor ";
            }
            if (!string.IsNullOrEmpty(Blog.BlogContent))
            {
               condition += " [BlogContent] = @BlogContent ";
            }

            if(condition.Length == 0)
            {
                return NotFound("No data found!");
            }

            condition = condition.Substring(0, condition.Length - 2);

            string query = $@"UPDATE [dbo].[Tbl_Blog]
                           SET {condition}
                         WHERE BlogId = @BlogId";

            Blog.BlogId = id;
            using IDbConnection db = new SqlConnection(ConnectionStrings.StringBuilder.ConnectionString);
            var result = db.Execute(query, Blog);

            var msg = result > 0 ? "patched success" : "failed";
            return Ok(msg);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            var item = FindData(id);
            if (item is null)
            {
                return NotFound("no data found!");
            }

            string query = "delete from [dbo].[Tbl_Blog] WHERE BlogId = @BlogId";
            using IDbConnection db = new SqlConnection(ConnectionStrings.StringBuilder.ConnectionString);
            var result = db.Execute(query, new BlogModel { BlogId = id });

            var msg = result > 0 ? "deleted success" : "failed";
            return Ok(msg);
        }

        private BlogModel? FindData(int id)
        {
            string query = "select * from tbl_blog where BlogId = @BlogId";
            using IDbConnection db = new SqlConnection(ConnectionStrings.StringBuilder.ConnectionString);
            var item = db.Query<BlogModel>(query, new { BlogId = id }).FirstOrDefault();
            return item;
        }
    }
}
