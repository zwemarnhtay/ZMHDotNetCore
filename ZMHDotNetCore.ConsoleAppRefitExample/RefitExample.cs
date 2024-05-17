using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZMHDotNetCore.ConsoleAppRefitExample.Models;

namespace ZMHDotNetCore.ConsoleAppRefitExample
{
    internal class RefitExample
    {
        private readonly IBlogAPI _service = RestService.For<IBlogAPI>("https://localhost:7033");

        public async Task runAsync()
        {
            await readAsync();
            await editAsync(3);
            await editAsync(808);
            await createAsync("Tote Tote", "Too", "blah......");
            await updateAsync(1, "Refit updated", "unknown", "...");
            await deleteAsync(12);
            await readAsync();
        }

        private async Task readAsync()
        {
            var blogs = await _service.getBlogs();
            foreach (var blog in blogs)
            {
                Console.WriteLine($"Id => {blog.blogId}");
                Console.WriteLine($"Title => {blog.blogTitle}");
                Console.WriteLine($"Author => {blog.blogAuthor}");
                Console.WriteLine($"Content => {blog.blogContent}");
                Console.WriteLine("---------------------------------");
            }
        }

        private async Task editAsync(int id)
        {
            try
            {
                var blog = await _service.getBlog(id);
                Console.WriteLine($"Id => {blog.blogId}");
                Console.WriteLine($"Title => {blog.blogTitle}");
                Console.WriteLine($"Author => {blog.blogAuthor}");
                Console.WriteLine($"Content => {blog.blogContent}");
                Console.WriteLine("---------------------------------");
            }
            catch(ApiException e)
            {
                Console.WriteLine(e.StatusCode.ToString());
                Console.WriteLine(e.Content);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        private async Task createAsync(string title, string author, string content)
        {
            blogModel blog = new blogModel()
            {
                blogTitle = title,
                blogAuthor = author,
                blogContent = content,
            };

            var msg = await _service.createBlog(blog);
            Console.WriteLine(msg);
        }

        private async Task updateAsync(int id, string title, string author, string content)
        {
            blogModel blog = new blogModel()
            {
                blogTitle = title,
                blogAuthor = author,
                blogContent = content,
            };
            try
            {
                var msg = await _service.updateBlog(id, blog);
                Console.WriteLine(msg);
            }
            catch (ApiException e)
            {
                Console.WriteLine(e.StatusCode.ToString());
                Console.WriteLine(e.Content);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private async Task deleteAsync(int id)
        {
            try
            {
                var msg = await _service.deleteBlog(id);
                Console.WriteLine(msg);
            }
            catch (ApiException e)
            {
                Console.WriteLine(e.StatusCode.ToString());
                Console.WriteLine(e.Content);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
