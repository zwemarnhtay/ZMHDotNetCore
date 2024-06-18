using Microsoft.EntityFrameworkCore;
using ZMHDotNetCore.MinimalAPI.Models;

namespace ZMHDotNetCore.MinimalAPI.Db;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Blog> Blogs { get; set; }
}
