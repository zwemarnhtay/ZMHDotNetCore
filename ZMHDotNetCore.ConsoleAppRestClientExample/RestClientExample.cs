using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ZMHDotNetCore.ConsoleAppHttpClientExample
{
    internal class RestClientExample
    {
        private readonly RestClient _client = new RestClient(new Uri("https://localhost:7041/"));
        private readonly string _blogEndPoint = "api/blog";

        public async Task RunAsync()
        {
            await ReadAsync();
            //await EditAsync(12);
            //await UpdateAsync(12, "HttpClient title", "blah....", "unknown");
            //await PatchAsync(12, author: "Holy Moly");
            //await EditAsync(12);
            //await DeleteAsync(9);
        }

        private async Task ReadAsync()
        {
            RestRequest restReq = new RestRequest(_blogEndPoint, Method.Get);
            var response = await _client.ExecuteAsync(restReq);
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = response.Content!;
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
            RestRequest restReq = new RestRequest($"{_blogEndPoint}/{id}", Method.Get);
            var response = await _client.ExecuteAsync(restReq);

            if (response.IsSuccessStatusCode)
            {
                var jsonStr = response.Content!;
                var blog = JsonConvert.DeserializeObject<BlogDTO>(jsonStr)!;

                Console.WriteLine($"Title => {blog.BlogTitle}");
                Console.WriteLine($"Content => {blog.BlogContent}");
                Console.WriteLine($"Author => {blog.BlogAuthor}");
                Console.Write("= = = = = = = = = \n");
            }
            else
            {
                var msg = response.Content!;
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

            RestRequest restReq = new RestRequest(_blogEndPoint, Method.Post);
            restReq.AddJsonBody(blogDTO);
            var response = await _client.ExecuteAsync(restReq);
            if(response.IsSuccessStatusCode)
            {
                var msg = response.Content!;
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

            RestRequest restReq = new RestRequest(_blogEndPoint, Method.Put);
            restReq.AddJsonBody(blogDTO);
            var response = await _client.ExecuteAsync(restReq);

            if (response.IsSuccessStatusCode)
            {
                var msg = response.Content;
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

            RestRequest restReq = new RestRequest(_blogEndPoint, Method.Patch);
            restReq.AddJsonBody(blogDTO);
            var response = await _client.ExecuteAsync(restReq);

            if (response.IsSuccessStatusCode)
            {
                var msg = response.Content;
                Console.WriteLine(msg);
            }
        }

        private async Task DeleteAsync(int id)
        {
            RestRequest restReq = new RestRequest($"{_blogEndPoint}/{id}", Method.Delete);
            var response = await _client.ExecuteAsync(restReq);

            if (response.IsSuccessStatusCode)
            {
                var msg = response.Content;
                Console.WriteLine(msg);
            }
            else
            {
                var msg = response.Content;
                Console.WriteLine(msg);
            }
        }

    }
}
