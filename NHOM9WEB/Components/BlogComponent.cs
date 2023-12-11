using Microsoft.AspNetCore.Mvc;
using NHOM9WEB.Models;

namespace NHOM9WEB.Components
{
    [ViewComponent(Name = "Blog")]

    public class BlogComponent : ViewComponent
    {
        private readonly DBNOITHATContext _context;
        public BlogComponent(DBNOITHATContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var items = _context.TbBlogs.Where(m => (bool)m.IsActive).OrderByDescending(i => i.BlogId).Take(5).ToList();
            ViewBag.blogComment = _context.TbBlogComments.Where(m => (bool)m.IsActive).ToList();
            return await Task.FromResult<IViewComponentResult>(View(items));
        }
    }
}
