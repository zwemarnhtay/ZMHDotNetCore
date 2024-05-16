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

public class OrderHeaderModel
{
    public int OrderId { get; set; }    
    public string InvoiceNo { get; set; }
    public int PizzaId { get; set;}
    public string Pizza { get; set; }
    public decimal Price { get; set; }
    public decimal TotalAmount { get; set;}
}

public class OrderItemModel
{
    public int OrderItemId { get; set; }
    public string InvoiceNo { get; set; }
    public int ItemId { get; set; }
    public string ExtraItem { get; set; }
    public decimal Price { get; set; }
}

public class ResponseOrder
{
    public OrderHeaderModel Order { get; set; }
    public List<OrderItemModel> OrderItem { get; set; }
}