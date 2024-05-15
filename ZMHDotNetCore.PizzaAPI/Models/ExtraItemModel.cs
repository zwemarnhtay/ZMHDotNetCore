using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZMHDotNetCore.PizzaAPI.Models
{

    [Table("Tbl_Item")]
    public class ExtraItemModel
    {
        [Key]
        [Column("ExtraItemId")]
        public int id { get; set; }

        [Column("ExtraItem")]
        public string name { get; set; }

        [Column("Price")]
        public decimal price { get; set; }
        [NotMapped]
        public string priceStr { get { return "$ " + price; } }
    }

}
