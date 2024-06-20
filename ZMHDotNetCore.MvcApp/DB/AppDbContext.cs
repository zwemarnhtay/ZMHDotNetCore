using Microsoft.EntityFrameworkCore;
using ZMHDotNetCore.MvcApp.Models;

namespace ZMHDotNetCore.MvcApp.DB;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<BlogModel> Blogs { get; set; }
}
