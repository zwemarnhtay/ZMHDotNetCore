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

        public List<blogModel> getBlogs()
        {
            var list = _context.Blogs.ToList();
            return list;
        }

        public blogModel getBlog(int id)
        {
            var blog = _context.Blogs.FirstOrDefault(x => x.blogId == id);
            return blog;
        }

        public int createBlog(blogModel reqBlog)
        {
            _context.Blogs.Add(reqBlog);
            var result = _context.SaveChanges();
            return result;
        }

        public int updateBlog(int id, blogModel reqBlog)
        {
            var blog = this.getBlog(id);
            if(blog == null) return 0;

            blog.blogTitle= reqBlog.blogTitle;
            blog.blogContent= reqBlog.blogContent;
            blog.blogAuthor= reqBlog.blogAuthor;

            var result = _context.SaveChanges();
            return result;
        }

        public int deleteBlog(int id)
        {
            var blog = this.getBlog(id);
            if(blog == null) return 0;

            _context.Blogs.Remove(blog);
            
            var result = _context.SaveChanges();
            return result;
        }
    }
}
