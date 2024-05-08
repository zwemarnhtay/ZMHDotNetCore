using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ZMHDotNetCore.RestAPIWithNLayer.Features.Blog
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly BL_Blog _blBlog;

        public BlogController()
        {
            _blBlog = new BL_Blog();
        }

        [HttpGet]
        public IActionResult getBlogs()
        {
            var list = _blBlog.getBlogs();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public IActionResult getBlog(int id)
        {
            var blog = _blBlog.getBlog(id);
            if (blog is null)
            {
                return NotFound("no data found");
            }
            return Ok(blog);
        }

        [HttpPut("{id}")]
        public IActionResult updateBlog(int id, blogModel blog)
        {
            var item = _blBlog.getBlog(id);
            if (item is null)
            {
                return NotFound("no data found");
            }

            var result = _blBlog.updateBlog(id, blog);

            var msg = result > 0 ? "updated success" : "failed";
            return Ok(msg);
        }

        [HttpPatch("{id}")]
        public IActionResult patchBlog(int id, blogModel blog)
        {
            var item = _blBlog.getBlog(id);
            if (item is null)
            {
                return NotFound("no data found");
            }

            var result = _blBlog.patchBlog(id, blog);

            var msg = result > 0 ? "updated success" : "failed";
            return Ok(msg);
        }

        [HttpDelete("{id}")]
        public IActionResult deleteBlog(int id)
        {
            var blog = _blBlog.getBlog(id);
            if (blog is null)
            {
                return NotFound("no data found");
            }

            var result = _blBlog.deleteBlog(id);

            var msg = result > 0 ? "deleted success" : "failed";
            return Ok(msg);
        }
    }
}
