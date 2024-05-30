// See https://aka.ms/new-console-template for more information

using Newtonsoft.Json;
using System.Text.Json.Serialization;
using ZMHDotNetCore.NLayer.BusinessLogic.Services;
using ZMHDotNetCore.NLayer.DataAccess.Models;

Console.WriteLine("Hello, World!");

BL_Blog bL_Blog = new BL_Blog();

List<Blog> List = bL_Blog.GetBlogs();
Console.WriteLine(List);

List<BlogEntity> blogEntities = List.Select(x => 
                                    new BlogEntity(x.BlogId, x.BlogTitle, x.BlogAuthor, x.BlogContent))     
                                    .ToList();

Console.WriteLine(blogEntities.ToString());

foreach (BlogEntity blogEntity in blogEntities)
{
    Console.WriteLine(blogEntity);
}

foreach(Blog item in List)
{
    Console.WriteLine(item);
}
var json = JsonConvert.SerializeObject(List, Formatting.Indented);
Console.WriteLine(json);

Console.ReadLine();