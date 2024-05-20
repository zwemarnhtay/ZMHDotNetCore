namespace ZMHDotNetCore.PizzaAPI.Queries
{
    public class PizzaQuery
    {
        public static string OrderQuery { get; } =@"SELECT orderPizza.*, pizza.Pizza, pizza.Price
                        FROM [dbo].[Tbl_PizzaOrder] orderPizza INNER JOIN [dbo].[Tbl_Pizza] pizza
                        ON pizza.PizzaId =  orderPizza.PizzaId WHERE InvoiceNo = @invoiceNo";

        public static string OrderItemQuery { get; } = @"SELECT orderItem.*, item.ExtraItem, item.Price
                    FROM [dbo].[Tbl_OrderItem] orderItem INNER JOIN [dbo].[Tbl_Item] item 
                    ON item.ExtraItemId =  orderItem.ItemId WHERE InvoiceNo = @invoiceNo";
    }
}
