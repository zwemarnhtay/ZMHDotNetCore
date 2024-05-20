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
        public IActionResult GetBlogs()
        {
            var list = _blBlog.GetBlogs();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            var blog = _blBlog.GetBlog(id);
            if (blog is null)
            {
                return NotFound("no data found");
            }
            return Ok(blog);
        }

        [HttpPost]
        public IActionResult CreateBlog(BlogModel blog)
        {
            var result = _blBlog.CreateBlog(blog);

            var msg = result > 0 ? "created success" : "created failed";
            return Ok(msg);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, BlogModel blog)
        {
            var item = _blBlog.GetBlog(id);
            if (item is null)
            {
                return NotFound("no data found");
            }

            var result = _blBlog.UpdateBlog(id, blog);

            var msg = result > 0 ? "updated success" : "failed";
            return Ok(msg);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, BlogModel blog)
        {
            var item = _blBlog.GetBlog(id);
            if (item is null)
            {
                return NotFound("no data found");
            }

            var result = _blBlog.PatchBlog(id, blog);

            var msg = result > 0 ? "updated success" : "failed";
            return Ok(msg);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            var blog = _blBlog.GetBlog(id);
            if (blog is null)
            {
                return NotFound("no data found");
            }

            var result = _blBlog.DeleteBlog(id);

            var msg = result > 0 ? "deleted success" : "failed";
            return Ok(msg);
        }
    }
}
