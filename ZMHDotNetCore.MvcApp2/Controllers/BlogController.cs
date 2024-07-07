﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZMHDotNetCore.MvcApp2.DB;
using ZMHDotNetCore.MvcApp2.Models;

namespace ZMHDotNetCore.MvcApp2.Controllers
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
            var list = await _dbContext.Blogs
                .AsNoTracking()
                .OrderByDescending(x => x.BlogId)
                .ToListAsync();
            return View(list);
        }

        [ActionName("Create")]
        public IActionResult BlogCreate()
        {
            return View("BlogCreate");
        }

        [HttpPost]
        [ActionName("Save")]
        public async Task<IActionResult> BlogCreate(BlogModel blog)
        {
            await _dbContext.AddAsync(blog);
            var result = await _dbContext.SaveChangesAsync();
            var message = new MessageModel()
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "Saving Successful." : "Saving Failed."
            };
            return Json(message);
        }

        [ActionName("Edit")]
        public async Task<IActionResult> BlogEdit(int id)
        {
            var blog = await _dbContext.Blogs.FirstOrDefaultAsync(x => x.BlogId == id);
            if (blog is null)
            {
                return Redirect("/Blog");
            }

            return View("BlogEdit", blog);
        }

        [HttpPost]
        [ActionName("Update")]
        public async Task<IActionResult> BlogUpdate(int id, BlogModel blog)
        {
            var item = await _dbContext.Blogs
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.BlogId == id);
            if (item is null)
            {
                return Redirect("/Blog");
            }

            item.BlogTitle = blog.BlogTitle;
            item.BlogAuthor = blog.BlogAuthor;
            item.BlogContent = blog.BlogContent;

            _dbContext.Entry(item).State = EntityState.Modified;

            var result = await _dbContext.SaveChangesAsync();
            var message = new MessageModel()
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "Updating Successful." : "Updating Failed."
            };
            return Json(message);
        }

        [ActionName("Delete")]
        public async Task<IActionResult> BlogDelete(int id)
        {
            var item = await _dbContext.Blogs.FirstOrDefaultAsync(x => x.BlogId == id);
            if (item is null)
            {
                return Redirect("/Blog");
            }

            _dbContext.Blogs.Remove(item);
            await _dbContext.SaveChangesAsync();

            return Redirect("/Blog");
        }
    }
}
