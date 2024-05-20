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

        public async Task RunAsync()
        {
            await ReadAsync();
            await EditAsync(3);
            await EditAsync(808);
            await CreateAsync("Tote Tote", "Too", "blah......");
            await UpdateAsync(1, "Refit updated", "unknown", "...");
            await DeleteAsync(12);
            await ReadAsync();
        }

        private async Task ReadAsync()
        {
            var blogs = await _service.GetBlogs();
            foreach (var blog in blogs)
            {
                Console.WriteLine($"Id => {blog.BlogId}");
                Console.WriteLine($"Title => {blog.BlogTitle}");
                Console.WriteLine($"Author => {blog.BlogAuthor}");
                Console.WriteLine($"Content => {blog.BlogContent}");
                Console.WriteLine("---------------------------------");
            }
        }

        private async Task EditAsync(int id)
        {
            try
            {
                var blog = await _service.GetBlog(id);
                Console.WriteLine($"Id => {blog.BlogId}");
                Console.WriteLine($"Title => {blog.BlogTitle}");
                Console.WriteLine($"Author => {blog.BlogAuthor}");
                Console.WriteLine($"Content => {blog.BlogContent}");
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

        private async Task CreateAsync(string title, string author, string content)
        {
            BlogModel blog = new BlogModel()
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content,
            };

            var msg = await _service.CreateBlog(blog);
            Console.WriteLine(msg);
        }

        private async Task UpdateAsync(int id, string title, string author, string content)
        {
            BlogModel blog = new BlogModel()
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content,
            };
            try
            {
                var msg = await _service.UpdateBlog(id, blog);
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

        private async Task DeleteAsync(int id)
        {
            try
            {
                var msg = await _service.DeleteBlog(id);
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
