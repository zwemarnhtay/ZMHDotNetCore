using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ZMHDotNetCore.ConsoleAppHttpClientExample
{
    internal class HttpClientExample
    {
        private readonly HttpClient _client = new HttpClient() { BaseAddress = new Uri("https://localhost:7041/") };
        private readonly string _blogEndPoint = "api/blog";

        public async Task runAsync()
        {
            await ReadAsync();
            //await EditAsync(8);
            //await UpdateAsync(8, "HttpClient title", "blah....", "unknown");
            //await PatchAsync(8, author: "Holy Moly");
            //await EditAsync(8);
            //await DeleteAsync(11);
        }

        private async Task ReadAsync()
        {
            var response = await _client.GetAsync(_blogEndPoint);

            if (response.IsSuccessStatusCode)
            {
                var jsonStr = await response.Content.ReadAsStringAsync();
                //Console.WriteLine(jsonStr);
                List<BlogDTO> blogList = JsonConvert.DeserializeObject<List<BlogDTO>>(jsonStr)!;

                foreach (var blog in blogList)
                {
                    Console.WriteLine($"Title => {blog.BlogTitle}");
                    Console.WriteLine($"Content => {blog.BlogContent}");
                    Console.WriteLine($"Author => {blog.BlogAuthor}");
                    Console.Write("= = = = = = = = = \n");
                }
            }
        }

        private async Task EditAsync(int id)
        {
            var response = await _client.GetAsync($"{_blogEndPoint}/{id}");

            if (response.IsSuccessStatusCode)
            {
                var jsonStr = await response.Content.ReadAsStringAsync();
                var blog = JsonConvert.DeserializeObject<BlogDTO>(jsonStr)!;

                Console.WriteLine($"Title => {blog.BlogTitle}");
                Console.WriteLine($"Content => {blog.BlogContent}");
                Console.WriteLine($"Author => {blog.BlogAuthor}");
                Console.Write("= = = = = = = = = \n");
            }
            else
            {
                var msg = await response.Content.ReadAsStringAsync();
                Console.WriteLine(msg);
            }
        }

        private async Task CreateAsync(string title, string content, string author)
        {
            BlogDTO blogDTO = new BlogDTO()
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };

            var jsonBlog = JsonConvert.SerializeObject(blogDTO);

            HttpContent httpContent = new StringContent(jsonBlog, Encoding.UTF8, Application.Json);

            var response = await _client.PostAsync(_blogEndPoint, httpContent);
            if(response.IsSuccessStatusCode)
            {
                var msg = response.Content.ReadAsStringAsync();
                Console.WriteLine(msg);
            }
        }

        private async Task UpdateAsync(int id, string title, string content, string author)
        {
            BlogDTO blogDTO = new BlogDTO()
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };
            var blogJson = JsonConvert.SerializeObject(blogDTO);

            HttpContent httpContent = new StringContent(blogJson, Encoding.UTF8, Application.Json);

            var response = await _client.PutAsync($"{_blogEndPoint}/{id}", httpContent);
            if (response.IsSuccessStatusCode)
            {
                var msg = response.Content.ReadAsStringAsync();
                Console.WriteLine(msg);
            }
        }

        private async Task PatchAsync(int id, string? title = null, string? content = null, string? author = null)
        {
            BlogDTO blogDTO = new BlogDTO();


            if (!string.IsNullOrEmpty(title))
            {
                blogDTO.BlogTitle = title;
            }
            if (!string.IsNullOrEmpty(content))
            {
                blogDTO.BlogContent = content;
            }
            if (!string.IsNullOrEmpty(author))
            {
                blogDTO.BlogAuthor = author;
            }
            var blogJson = JsonConvert.SerializeObject(blogDTO);

            HttpContent httpContent = new StringContent(blogJson, Encoding.UTF8, Application.Json);

            var response = await _client.PatchAsync($"{_blogEndPoint}/{id}", httpContent);
            if (response.IsSuccessStatusCode)
            {
                var msg = response.Content.ReadAsStringAsync();
                Console.WriteLine(msg);
            }
        }

        private async Task DeleteAsync(int id)
        {
            var response = await _client.DeleteAsync($"{_blogEndPoint}/{id}");
            if (response.IsSuccessStatusCode)
            {
                var msg = await response.Content.ReadAsStringAsync();
                Console.WriteLine(msg);
            }
            else
            {
                var msg = await response.Content.ReadAsStringAsync();
                Console.WriteLine(msg);
            }
        }

    }
}
