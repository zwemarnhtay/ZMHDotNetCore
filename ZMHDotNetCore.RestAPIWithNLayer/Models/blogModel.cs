namespace ZMHDotNetCore.RestAPIWithNLayer.Models
{
    [Table("tbl_blog")]
    public class BlogModel
    {
        [Key]
        public int BlogId { get; set; }
        public string? BlogTitle { get; set; }
        public string? BlogAuthor { get; set; }
        public string? BlogContent { get; set; }
    }
}
