using System;
using System.Collections.Generic;

namespace ZMHDotNetCore.ConsoleAppEFCore.Db.Models;

public partial class TblPizzaOrder
{
    public int OrderId { get; set; }

    public string InvoiceNo { get; set; } = null!;

    public int PizzaId { get; set; }

    public decimal TotalAmount { get; set; }
}
