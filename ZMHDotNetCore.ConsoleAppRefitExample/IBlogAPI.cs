using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZMHDotNetCore.ConsoleAppRefitExample.Models;

namespace ZMHDotNetCore.ConsoleAppRefitExample
{
    public interface IBlogAPI
    {
        [Get("/api/Blog")]
        Task<List<blogModel>> getBlogs();

        [Get("/api/Blog/{id}")]
        Task<blogModel> getBlog(int id);

        [Post("/api/Blog")]
        Task<string> createBlog(blogModel blog);

        [Put("/api/Blog/{id}")]
        Task<string> updateBlog(int id, blogModel blog);

        [Delete("/api/Blog/{id}")]
        Task<string> deleteBlog(int id);
    }
}
