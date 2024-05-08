namespace ZMHDotNetCore.RestAPIWithNLayer.Features.Blog
{
    public class BL_Blog
    {
        private readonly DA_Blog _daBlog;

        public BL_Blog()
        {
            _daBlog = new DA_Blog();
        }

        public List<blogModel> getBlogs()
        {
            var list = _daBlog.getBlogs();
            return list;
        }

        public blogModel getBlog(int id)
        {
            var blog = _daBlog.getBlog(id);
            return blog;
        }

        public int createBlog(blogModel reqBlog)
        {
            var result = _daBlog.createBlog(reqBlog);
            return result;
        }

        public int updateBlog(int id, blogModel reqBlog)
        {
            var result = _daBlog.updateBlog(id, reqBlog);
            return result;
        }

        public int deleteBlog(int id)
        {
            var result = (_daBlog.deleteBlog(id));
            return result;
        }

    }
}
