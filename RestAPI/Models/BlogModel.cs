using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestAPI.Models;

[Table("Tbl_Blog")]

public class blogModel
{

    [Key]
    public int BlogId { get; set; }
    public string? BlogTitle { get; set; }
    public string? BlogAuthor { get; set; }
    public string? BlogContent { get; set; }
}
//instead of above old school, can use the below method!
//public record blogEntity(int blogId, string blogTitle, string blogAuthor, string blodContent);
