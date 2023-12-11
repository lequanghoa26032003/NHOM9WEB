using Microsoft.AspNetCore.Mvc;
using NHOM9WEB.Models;

namespace NHOM9WEB.Components
{
    [ViewComponent(Name = "RecentPost")]
    public class RecentPostComponent : ViewComponent
    {
        private readonly DBNOITHATContext _context;
        public RecentPostComponent(DBNOITHATContext context)
        {
            _context=context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var listofPost = (from p in _context.TbBlogs
                              where (p.IsActive == true) &&(p.IsActive==true)
                              orderby p.BlogId descending
                              select p).Take(6).ToList();
            return await Task.FromResult((IViewComponentResult)View("Default", listofPost));
        }
    }
}
