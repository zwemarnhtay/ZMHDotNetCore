using Microsoft.EntityFrameworkCore;
using ZMHDotNetCore.MvcApp2.Models;

namespace ZMHDotNetCore.MvcApp2.DB;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<BlogModel> Blogs { get; set; }
}
