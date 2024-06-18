using Microsoft.EntityFrameworkCore;
using ZMHDotNetCore.MinimalAPI.Db;
using ZMHDotNetCore.MinimalAPI.Models;

namespace ZMHDotNetCore.MinimalAPI.Services;

public static class BlogService
{
    public static IEndpointRouteBuilder MapBlogs(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/Blog", async (AppDbContext db) =>
        {
            var list = await db.Blogs.AsNoTracking().ToListAsync();
            return Results.Ok(list);
        });

        app.MapGet("/api/Blog/{id}", async (AppDbContext db, int id) =>
        {
            var blog = await db.Blogs.AsNoTracking().FirstOrDefaultAsync(x => x.BlogId == id);
            return Results.Ok(blog);
        });

        app.MapPost("/api/Blog", async (AppDbContext db, Blog blog) =>
        {
            await db.Blogs.AddAsync(blog);
            var result = await db.SaveChangesAsync();

            var msg = result > 0 ? "added success" : "failed";
            return Results.Ok(msg);
        });

        app.MapPut("/api/Blog/{id}", async (AppDbContext db, int id, Blog blog) =>
        {
            var item = await db.Blogs.FirstOrDefaultAsync(x => x.BlogId == id);
            if (item == null) return Results.NotFound("no data found");

            item.BlogTitle = blog.BlogTitle;
            item.BlogContent = blog.BlogContent;
            item.BlogAuthor = blog.BlogAuthor;

            var result = await db.SaveChangesAsync();

            var msg = result > 0 ? "updated success" : "failed";
            return Results.Ok(msg);
        });

        app.MapDelete("/api/Blog/{id}", async (AppDbContext db, int id) =>
        {
            var item = await db.Blogs.FirstOrDefaultAsync(x => x.BlogId == id);
            if (item == null) return Results.NotFound("no data found");

            db.Blogs.Remove(item);

            var result = await db.SaveChangesAsync();

            var msg = result > 0 ? "deleted success" : "failed";
            return Results.Ok(msg);
        });

        return app;
    }
}
