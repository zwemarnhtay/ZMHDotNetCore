using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZMHDotNetCore.ConsoleApp
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
