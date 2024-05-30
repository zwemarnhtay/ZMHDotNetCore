using Microsoft.EntityFrameworkCore;
using ZMHDotNetCore.NLayer.DataAccess.Models;

namespace ZMHDotNetCore.NLayer.DataAccess.DB
{
    internal class DBContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(DBConnection.ConnectionBuilder.ConnectionString);
        }
        public DbSet<Blog> Blogs { get; set; }
    }
}
