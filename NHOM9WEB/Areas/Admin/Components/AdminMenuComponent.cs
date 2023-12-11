using Microsoft.AspNetCore.Mvc;
using NHOM9WEB.Models;

namespace NHOM9WEB.Areas.Admin.Components
{
    [ViewComponent(Name = "AdminMenu")]
    public class AdminMenuComponent : ViewComponent
    {
        private readonly DBNOITHATContext _context;
        public AdminMenuComponent(DBNOITHATContext context)
        {
            _context=context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var mnList = (from mn in _context.AdminMenus
                          where (mn.IsActive==true)
                          select mn).ToList();
            return await Task.FromResult((IViewComponentResult)View("Default", mnList));
        }
    }
}
