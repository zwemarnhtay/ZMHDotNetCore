using System;
using System.Collections.Generic;

namespace ZMHDotNetCore.ConsoleAppEFCore.Db.Models;

public partial class TblOrderDetail
{
    public int OrderDetailId { get; set; }

    public string InvoiceNo { get; set; } = null!;

    public int ItemId { get; set; }
}
