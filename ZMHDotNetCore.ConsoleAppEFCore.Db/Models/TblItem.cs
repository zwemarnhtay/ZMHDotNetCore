using System;
using System.Collections.Generic;

namespace ZMHDotNetCore.ConsoleAppEFCore.Db.Models;

public partial class TblItem
{
    public int ExtraItemId { get; set; }

    public string ExtraItem { get; set; } = null!;

    public decimal Price { get; set; }
}
