using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZMHDotNetCore.PizzaAPI.Models
{
    [Table("Tbl_PizzaOrder")]
    public class OrderModel
    {
        [Key]
        public int OrderId { get; set; }
        public string? InvoiceNo { get; set; }
        public int PizzaId { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
