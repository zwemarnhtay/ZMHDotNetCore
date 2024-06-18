using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ZMHDotNetCore.MinimalAPI.Models;

[Table("Tbl_Blog")]

public class Blog
{
    [Key]
    public int BlogId { get; set; }
    public string? BlogTitle { get; set; }
    public string? BlogAuthor { get; set; }
    public string? BlogContent { get; set; }
}
