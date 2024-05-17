using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZMHDotNetCore.ConsoleAppRefitExample.Models
{
    public class blogModel
    {
        public int blogId { get; set; }
        public string? blogTitle { get; set; }
        public string? blogAuthor { get; set; }
        public string? blogContent { get; set; }
    }
}
