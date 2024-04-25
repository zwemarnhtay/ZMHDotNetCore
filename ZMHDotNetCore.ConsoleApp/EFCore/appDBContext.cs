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
    internal class appDBContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionStrings.stringBuilder.ConnectionString);
        }
        public DbSet<blogDTO> Blogs { get; set; }
    }
}
