using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZMHDotNetCore.ConsoleApp.DTO;
using ZMHDotNetCore.ConsoleApp.Services;

namespace ZMHDotNetCore.ConsoleApp.EFCore
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions options) : base(options)
        {
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(ConnectionStrings.stringBuilder.ConnectionString);
        //}
        public DbSet<BlogDTO> Blogs { get; set; }
    }
}
