// See https://aka.ms/new-console-template for more information
using System.Data;
using System.Data.SqlClient;
using ZMHDotNetCore.ConsoleApp.AdoDotNet;
using ZMHDotNetCore.ConsoleApp.EFCore;

Console.WriteLine("Hello, World!");


AdoDotNetCRUD adoCRUD = new AdoDotNetCRUD();
//adoCRUD.update(1, "titleUU", "ContentBlah", "unknown");
//adoCRUD.update(13, "Second_title", "Content_letter", "the only one");
//adoCRUD.delete(12);
//adoCRUD.edit(3);

//dapperExample CRUD = new dapperExample();
//CRUD.run();
EFCoreExample CRUD = new EFCoreExample();
CRUD.run();
 
Console.ReadKey();