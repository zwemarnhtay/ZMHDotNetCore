// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using ZMHDotNetCore.ConsoleAppHttpClientExample;

Console.WriteLine("Hello, World!");


HttpClientExample clientExample = new HttpClientExample();

await clientExample.runAsync();

Console.ReadLine();