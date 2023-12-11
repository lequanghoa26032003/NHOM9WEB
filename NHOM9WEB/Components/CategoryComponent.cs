using Microsoft.AspNetCore.Mvc;
using NHOM9WEB.Models;

namespace NHOM9WEB.Components
{
    [ViewComponent(Name = "Category")]

    public class CategoryComponent : ViewComponent
    {
        private readonly DBNOITHATContext _context;
        public CategoryComponent(DBNOITHATContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var listofPost = (from p in _context.TbProductCategories
                              where (p.IsActive == true)
                              orderby p.CategoryProductId descending
                              select p).Take(6).ToList();
            return await Task.FromResult((IViewComponentResult)View("Default", listofPost));
        }
    }
}

