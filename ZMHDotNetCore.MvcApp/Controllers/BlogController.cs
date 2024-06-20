using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZMHDotNetCore.MvcApp.DB;

namespace ZMHDotNetCore.MvcApp.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppDbContext _dbContext;

        public BlogController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _dbContext.Blogs.ToListAsync();
            return  View(list);
        }
    }
}
