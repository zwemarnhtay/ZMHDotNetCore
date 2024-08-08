using System;
using System.Collections.Generic;

namespace ZMHDotNetCore.ConsoleAppEFCore.Db.Models;

public partial class TblOrderItem
{
    public int OrderItemId { get; set; }

    public string InvoiceNo { get; set; } = null!;

    public int ItemId { get; set; }
}
