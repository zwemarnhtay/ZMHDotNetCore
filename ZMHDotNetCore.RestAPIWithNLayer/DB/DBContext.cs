namespace ZMHDotNetCore.RestAPIWithNLayer.DB
{
    internal class DBContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(dbconnection.connectionBuilder.ConnectionString);
        }
        public DbSet<blogModel> Blogs { get; set; }
    }
}
