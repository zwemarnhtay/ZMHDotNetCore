namespace ZMHDotNetCore.PizzaAPI.Models
{
    public class OrderRequestModel
    {
        public int PizzaId { get; set; }
        public int[] Extras { get; set; }
    }
}
