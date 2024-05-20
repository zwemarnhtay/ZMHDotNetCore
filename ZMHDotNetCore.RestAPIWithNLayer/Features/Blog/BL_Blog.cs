namespace ZMHDotNetCore.RestAPIWithNLayer.Features.Blog
{
    public class BL_Blog
    {
        private readonly DA_Blog _daBlog;

        public BL_Blog()
        {
            _daBlog = new DA_Blog();
        }

        public List<BlogModel> GetBlogs()
        {
            var list = _daBlog.GetBlogs();
            return list;
        }

        public BlogModel GetBlog(int id)
        {
            var blog = _daBlog.GetBlog(id);
            return blog;
        }

        public int CreateBlog(BlogModel reqBlog)
        {
            var result = _daBlog.CreateBlog(reqBlog);
            return result;
        }

        public int UpdateBlog(int id, BlogModel reqBlog)
        {
            var result = _daBlog.UpdateBlog(id, reqBlog);
            return result;
        }

        public int PatchBlog(int id, BlogModel reqBlog)
        {
            var result = _daBlog.PatchBlog(id, reqBlog);
            return result;
        }

        public int DeleteBlog(int id)
        {
            var result = (_daBlog.DeleteBlog(id));
            return result;
        }
    }
}
