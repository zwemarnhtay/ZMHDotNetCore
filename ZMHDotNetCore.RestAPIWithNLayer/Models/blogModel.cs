namespace ZMHDotNetCore.RestAPIWithNLayer.Models
{
    [Table("tbl_blog")]
    public class blogModel
    {
        [Key]
        public int blogId { get; set; }
        public string? blogTitle { get; set; }
        public string? blogAuthor { get; set; }
        public string? blogContent { get; set; }
    }
}
