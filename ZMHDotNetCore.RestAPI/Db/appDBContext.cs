using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZMHDotNetCore.RestAPI;
using ZMHDotNetCore.RestAPI.Models;

namespace RestAPI.Db
{
    internal class appDBContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionStrings.stringBuilder.ConnectionString);
        }
        public DbSet<blogModel> Blogs { get; set; }
    }
}
