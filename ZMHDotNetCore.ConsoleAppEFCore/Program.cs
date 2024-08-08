using ZMHDotNetCore.ConsoleAppEFCore.Db.Models;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

AppDbContext db = new AppDbContext();

db.TblPieCharts.ToList().ForEach(x =>
{
    Console.WriteLine($"{x.PieChartId} -> {x.PieChartName}");
});

Console.ReadKey();