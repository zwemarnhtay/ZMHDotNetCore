using ZMHDotNetCore.RestAPIWithNLayer.DB;

namespace ZMHDotNetCore.RestAPIWithNLayer.Features.Blog
{
    public class DA_Blog
    {
        private readonly DBContext _context;

        public DA_Blog()
        {
            _context = new DBContext();
        }

        public List<BlogModel> GetBlogs()
        {
            var list = _context.Blogs.ToList();
            return list;
        }

        public BlogModel GetBlog(int id)
        {
                var blog = _context.Blogs.FirstOrDefault(x => x.BlogId == id);
            return blog;
        }

        public int CreateBlog(BlogModel reqBlog)
        {
            _context.Blogs.Add(reqBlog);
            var result = _context.SaveChanges();
            return result;
        }

        public int UpdateBlog(int id, BlogModel reqBlog)
        {
            var blog = this.GetBlog(id);
            if(blog == null) return 0;

            blog.BlogTitle= reqBlog.BlogTitle;
            blog.BlogContent= reqBlog.BlogContent;
            blog.BlogAuthor= reqBlog.BlogAuthor;

            var result = _context.SaveChanges();
            return result;
        }

        public int PatchBlog(int id, BlogModel reqBlog) 
        {
            var blog = this.GetBlog(id);
            if(blog == null) return 0;

            if (!string.IsNullOrEmpty(reqBlog.BlogTitle))
            {
                blog.BlogTitle = reqBlog.BlogTitle;
            }
            if (!string.IsNullOrEmpty(reqBlog.BlogAuthor))
            {
                blog.BlogAuthor = reqBlog.BlogAuthor;
            }
            if (!string.IsNullOrEmpty(reqBlog.BlogContent))
            {
                blog.BlogContent = reqBlog.BlogContent;
            }

            var result = _context.SaveChanges();
            return result;
        }

        public int DeleteBlog(int id)
        {
            var blog = this.GetBlog(id);
            if(blog == null) return 0;

            _context.Blogs.Remove(blog);
            
            var result = _context.SaveChanges();
            return result;
        }
    }
}
