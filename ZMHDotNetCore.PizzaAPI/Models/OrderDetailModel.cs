using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZMHDotNetCore.PizzaAPI.Models
{
    [Table("Tbl_OrderItem")]
    public class OrderDetailModel
    {
        [Key]
        public int OrderItemId { get; set; }
        public string InvoiceNo { get; set; }
        public int ItemId { get; set; }
    }
}
