using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZMHDotNetCore.NLayer.DataAccess.DB;
using ZMHDotNetCore.NLayer.DataAccess.Models;

namespace ZMHDotNetCore.NLayer.DataAccess.Services
{
    public class DA_Blog
    {
        private readonly DBContext _context;

        public DA_Blog()
        {
            _context = new DBContext();
        }

        public List<Blog> GetBlogs()
        {
            var list = _context.Blogs.ToList();
            return list;
        }

        public Blog GetBlog(int id)
        {
            var blog = _context.Blogs.FirstOrDefault(x => x.BlogId == id);
            return blog;
        }

        public int CreateBlog(Blog reqBlog)
        {
            _context.Blogs.Add(reqBlog);
            var result = _context.SaveChanges();
            return result;
        }

        public int UpdateBlog(int id, Blog reqBlog)
        {
            var blog = this.GetBlog(id);
            if (blog == null) return 0;

            blog.BlogTitle = reqBlog.BlogTitle;
            blog.BlogContent = reqBlog.BlogContent;
            blog.BlogAuthor = reqBlog.BlogAuthor;

            var result = _context.SaveChanges();
            return result;
        }

        public int PatchBlog(int id, Blog reqBlog)
        {
            var blog = this.GetBlog(id);
            if (blog == null) return 0;

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
            if (blog == null) return 0;

            _context.Blogs.Remove(blog);

            var result = _context.SaveChanges();
            return result;
        }
    }
}
