using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ZMHDotNetCore.PizzaAPI.Models;

namespace ZMHDotNetCore.PizzaAPI.DB
{
    internal class DBContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(dbconnection.connectionBuilder.ConnectionString);
        }

        public DbSet<ExtraItemModel> ExtraItems { get; set; }
        public DbSet<OrderModel> Orders { get; set; }
        public DbSet<OrderDetailModel> OrderDetail { get; set; }
        public DbSet<PizzaModel> Pizzas { get; set; }
    }
}
