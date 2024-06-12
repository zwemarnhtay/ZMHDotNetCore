// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using System.Data.SqlClient;
using ZMHDotNetCore.ConsoleApp.AdoDotNet;
using ZMHDotNetCore.ConsoleApp.Dapper;
using ZMHDotNetCore.ConsoleApp.EFCore;
using ZMHDotNetCore.ConsoleApp.Services;

Console.WriteLine("Hello, World!");


var connectionString = ConnectionStrings.stringBuilder.ConnectionString;
var sqlConnectionStringBuilder = new SqlConnectionStringBuilder(connectionString);

var serviceProvider = new ServiceCollection()
    .AddScoped<AdoDotNetCRUD>(ado => new AdoDotNetCRUD(sqlConnectionStringBuilder))
    .AddScoped<DapperExample>(dapper => new DapperExample(sqlConnectionStringBuilder))
    .AddDbContext<AppDBContext>(opt =>
    {
        opt.UseSqlServer(connectionString);
    })
    .AddScoped<EFCoreExample>()
    .BuildServiceProvider();

AppDBContext dbContext = serviceProvider.GetRequiredService<AppDBContext>();

AdoDotNetCRUD adoCRUD = serviceProvider.GetRequiredService<AdoDotNetCRUD>();
//adoCRUD.update(1, "titleUU", "ContentBlah", "unknown");
//adoCRUD.update(13, "Second_title", "Content_letter", "the only one");
//adoCRUD.delete(12);
//adoCRUD.edit(3);

//dapperExample CRUD = new dapperExample();
//CRUD.run();
EFCoreExample CRUD = serviceProvider.GetRequiredService<EFCoreExample>();
CRUD.run();

Console.ReadKey();