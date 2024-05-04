﻿using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using ZMHDotNetCore.RestAPI.Models;
using ZMHDotNetCore.Shared;

namespace ZMHDotNetCore.RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogDapper2Controller : ControllerBase
    {
        private readonly DapperServices _dapperService = new DapperServices(connectionStrings.stringBuilder.ConnectionString);
        [HttpGet]
        public IActionResult getBlogs()
        {
            string query = "SELECT * FROM tbl_Blog";
            var list = _dapperService.Query<blogModel>(query);
            return Ok(list);
        }

        [HttpGet("{id}")]
        public IActionResult getBlog(int id)
        {
            var item = findData(id);
            if(item is null)
            {
                return NotFound("no data found!");
            }

            return Ok(item);
        }

        [HttpPost]
        public IActionResult createBlog(blogModel Blog)
        {
            string query = @"INSERT INTO [dbo].[Tbl_Blog]
                           ([BlogTitle]
                           ,[BlogAuthor]
                           ,[BlogContent])
                     VALUES
                            (@BlogTitle,
                             @BlogAuthor,
                             @BlogContent)";
            int result = _dapperService.Execute(query, Blog);

            string msg = result > 0 ? "created success" : "failed";
            return Ok(msg);
        }

        [HttpPut("{id}")]
        public IActionResult updateBlog(int id, blogModel Blog)
        {
            var item = findData(id);
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
            var result = _dapperService.Execute(query, Blog);

            var msg = result > 0 ? "updated success" : "failed";
            return Ok(msg);
        }

        [HttpPatch("{id}")]
        public IActionResult patchBlog(int id, blogModel Blog)
        {
            var item = findData(id);
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
            var result = _dapperService.Execute(query, Blog);

            var msg = result > 0 ? "patched success" : "failed";
            return Ok(msg);
        }

        [HttpDelete("{id}")]
        public IActionResult deleteBlog(int id)
        {
            var item = findData(id);
            if (item is null)
            {
                return NotFound("no data found!");
            }

            string query = "delete from [dbo].[Tbl_Blog] WHERE BlogId = @BlogId";
            var result = _dapperService.Execute(query, new blogModel { BlogId = id });

            var msg = result > 0 ? "deleted success" : "failed";
            return Ok(msg);
        }

        private blogModel? findData(int id)
        {
            string query = "select * from tbl_blog where BlogId = @BlogId";
            using IDbConnection db = new SqlConnection(connectionStrings.stringBuilder.ConnectionString);
            var item = _dapperService.QueryFirstOrDefault<blogModel>(query, new { BlogId = id });
            return item;
        }
    }
}
