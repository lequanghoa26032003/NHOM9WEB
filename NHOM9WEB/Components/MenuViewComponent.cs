using Microsoft.AspNetCore.Mvc;
using NHOM9WEB.Models;

namespace NHOM9WEB.Components
{
    [ViewComponent(Name = "MenuView")]

    public class MenuViewComponent : ViewComponent
    {
        private DBNOITHATContext _context;
        public MenuViewComponent(DBNOITHATContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var listofMenu = (from m in _context.TbMenus
                              where (m.IsActive == true) && (m.Position == 1)
                              select m).ToList();
            return await Task.FromResult((IViewComponentResult)View("Default", listofMenu));
        }
    }
}
