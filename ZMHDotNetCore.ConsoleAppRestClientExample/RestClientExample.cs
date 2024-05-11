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

        public async Task runAsync()
        {
            await readAsync();
            //await editAsync(12);
            //await updateAsync(12, "HttpClient title", "blah....", "unknown");
            //await patchAsync(12, author: "Holy Moly");
            //await editAsync(12);
            //await deleteAsync(9);
        }

        private async Task readAsync()
        {
            RestRequest restReq = new RestRequest(_blogEndPoint, Method.Get);
            var response = await _client.ExecuteAsync(restReq);
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = response.Content!;
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
            RestRequest restReq = new RestRequest($"{_blogEndPoint}/{id}", Method.Get);
            var response = await _client.ExecuteAsync(restReq);

            if (response.IsSuccessStatusCode)
            {
                var jsonStr = response.Content!;
                var blog = JsonConvert.DeserializeObject<blogDTO>(jsonStr)!;

                Console.WriteLine($"Title => {blog.blogTitle}");
                Console.WriteLine($"Content => {blog.blogContent}");
                Console.WriteLine($"Author => {blog.blogAuthor}");
                Console.Write("= = = = = = = = = \n");
            }
            else
            {
                var msg = response.Content!;
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

            RestRequest restReq = new RestRequest(_blogEndPoint, Method.Post);
            restReq.AddJsonBody(blogDTO);
            var response = await _client.ExecuteAsync(restReq);
            if(response.IsSuccessStatusCode)
            {
                var msg = response.Content!;
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

            RestRequest restReq = new RestRequest(_blogEndPoint, Method.Put);
            restReq.AddJsonBody(blogDTO);
            var response = await _client.ExecuteAsync(restReq);

            if (response.IsSuccessStatusCode)
            {
                var msg = response.Content;
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

            RestRequest restReq = new RestRequest(_blogEndPoint, Method.Patch);
            restReq.AddJsonBody(blogDTO);
            var response = await _client.ExecuteAsync(restReq);

            if (response.IsSuccessStatusCode)
            {
                var msg = response.Content;
                Console.WriteLine(msg);
            }
        }

        private async Task deleteAsync(int id)
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
