namespace ZMHDotNetCore.PizzaAPI.Models
{
    public class InvoiceModel
    {
        public string Message { get; set; }
        public string InvoiceNo { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
