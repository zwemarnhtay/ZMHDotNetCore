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
            await readAsync();
            //await editAsync(8);
            //await updateAsync(8, "HttpClient title", "blah....", "unknown");
            //await patchAsync(8, author: "Holy Moly");
            //await editAsync(8);
            //await deleteAsync(11);
        }

        private async Task readAsync()
        {
            var response = await _client.GetAsync(_blogEndPoint);

            if (response.IsSuccessStatusCode)
            {
                var jsonStr = await response.Content.ReadAsStringAsync();
                //Console.WriteLine(jsonStr);
                List<blogDTO> blogList = JsonConvert.DeserializeObject<List<blogDTO>>(jsonStr)!;

                foreach (var blog in blogList)
                {
                    Console.WriteLine($"Title => {blog.blogTitle}");
                    Console.WriteLine($"Content => {blog.blogContent}");
                    Console.WriteLine($"Author => {blog.blogAuthor}");
                    Console.Write("= = = = = = = = = \n");
                }
            }
        }

        private async Task editAsync(int id)
        {
            var response = await _client.GetAsync($"{_blogEndPoint}/{id}");

            if (response.IsSuccessStatusCode)
            {
                var jsonStr = await response.Content.ReadAsStringAsync();
                var blog = JsonConvert.DeserializeObject<blogDTO>(jsonStr)!;

                Console.WriteLine($"Title => {blog.blogTitle}");
                Console.WriteLine($"Content => {blog.blogContent}");
                Console.WriteLine($"Author => {blog.blogAuthor}");
                Console.Write("= = = = = = = = = \n");
            }
            else
            {
                var msg = await response.Content.ReadAsStringAsync();
                Console.WriteLine(msg);
            }
        }

        private async Task createAsync(string title, string content, string author)
        {
            blogDTO blogDTO = new blogDTO()
            {
                blogTitle = title,
                blogAuthor = author,
                blogContent = content
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

        private async Task updateAsync(int id, string title, string content, string author)
        {
            blogDTO blogDTO = new blogDTO()
            {
                blogTitle = title,
                blogAuthor = author,
                blogContent = content
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

        private async Task patchAsync(int id, string? title = null, string? content = null, string? author = null)
        {
            blogDTO blogDTO = new blogDTO();


            if (!string.IsNullOrEmpty(title))
            {
                blogDTO.blogTitle = title;
            }
            if (!string.IsNullOrEmpty(content))
            {
                blogDTO.blogContent = content;
            }
            if (!string.IsNullOrEmpty(author))
            {
                blogDTO.blogAuthor = author;
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

        private async Task deleteAsync(int id)
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
