using System;
using System.Collections.Generic;

namespace ZMHDotNetCore.ConsoleAppEFCore.Db.Models;

public partial class TblOrder
{
    public int PizzaOrderId { get; set; }

    public string PizzaOrderInvoiceNo { get; set; } = null!;

    public int PizzaId { get; set; }

    public decimal TotalAmount { get; set; }
}
