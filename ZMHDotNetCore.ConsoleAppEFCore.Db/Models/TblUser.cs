using System;
using System.Collections.Generic;

namespace ZMHDotNetCore.ConsoleAppEFCore.Db.Models;

public partial class TblUser
{
    public int Id { get; set; }

    public string Password { get; set; } = null!;

    public bool IsAdmin { get; set; }

    public string Email { get; set; } = null!;
}
