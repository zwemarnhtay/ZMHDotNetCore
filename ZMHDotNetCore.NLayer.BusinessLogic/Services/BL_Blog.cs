using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZMHDotNetCore.NLayer.DataAccess.Models;
using ZMHDotNetCore.NLayer.DataAccess.Services;

namespace ZMHDotNetCore.NLayer.BusinessLogic.Services
{
    public class BL_Blog
    {
        private readonly DA_Blog _daBlog;

        public BL_Blog()
        {
            _daBlog = new DA_Blog();
        }

        public List<Blog> GetBlogs()
        {
            var list = _daBlog.GetBlogs();
            return list;
        }

        public Blog GetBlog(int id)
        {
            var blog = _daBlog.GetBlog(id);
            return blog;
        }

        public int CreateBlog(Blog reqBlog)
        {
            var result = _daBlog.CreateBlog(reqBlog);
            return result;
        }

        public int UpdateBlog(int id, Blog reqBlog)
        {
            var result = _daBlog.UpdateBlog(id, reqBlog);
            return result;
        }

        public int PatchBlog(int id, Blog reqBlog)
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
