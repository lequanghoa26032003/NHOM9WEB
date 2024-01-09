using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NHOM9WEB.Models;

namespace NHOM9WEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MenusController : Controller
    {
        private readonly DBNOITHATContext _context;

        public MenusController(DBNOITHATContext context)
        {
            _context = context;
        }

        // GET: Admin/Menus
        public async Task<IActionResult> Index()
        {
            return _context.TbMenus != null ?
                        View(await _context.TbMenus.ToListAsync()) :
                        Problem("Entity set 'DBNOITHATContext.TbMenus'  is null.");
        }

        // GET: Admin/Menus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TbMenus == null) {
                return NotFound();
            }

            var tbMenu = await _context.TbMenus
                .FirstOrDefaultAsync(m => m.MenuId == id);
            if (tbMenu == null) {
                return NotFound();
            }

            return View(tbMenu);
        }

        // GET: Admin/Menus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Menus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MenuId,MenuName,IsActive,ControllerName,ActionName,Levels,ParentId,Link,MenuOrder,Position")] TbMenu tbMenu)
        {
            if (ModelState.IsValid) {
                _context.Add(tbMenu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tbMenu);
        }

        // GET: Admin/Menus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TbMenus == null) {
                return NotFound();
            }

            var tbMenu = await _context.TbMenus.FindAsync(id);
            if (tbMenu == null) {
                return NotFound();
            }
            return View(tbMenu);
        }

        // POST: Admin/Menus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MenuId,MenuName,IsActive,ControllerName,ActionName,Levels,ParentId,Link,MenuOrder,Position")] TbMenu tbMenu)
        {
            if (id != tbMenu.MenuId) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(tbMenu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException) {
                    if (!TbMenuExists(tbMenu.MenuId)) {
                        return NotFound();
                    }
                    else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tbMenu);
        }

        // GET: Admin/Menus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TbMenus == null) {
                return NotFound();
            }

            var tbMenu = await _context.TbMenus
                .FirstOrDefaultAsync(m => m.MenuId == id);
            if (tbMenu == null) {
                return NotFound();
            }

            return View(tbMenu);
        }

        // POST: Admin/Menus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TbMenus == null) {
                return Problem("Entity set 'DBNOITHATContext.TbMenus'  is null.");
            }
            var tbMenu = await _context.TbMenus.FindAsync(id);
            if (tbMenu != null) {
                _context.TbMenus.Remove(tbMenu);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public IActionResult ToggleIsActive(int id)
        {
            var menu = _context.TbMenus.Find(id);

            if (menu != null) {
                // Chuyển đổi trạng thái
                menu.IsActive = !menu.IsActive;


                _context.SaveChanges();


                return Json(true);
            }


            return Json(false);
        }
        [HttpPost]
        public IActionResult DeleteMenu(int id)
        {
            try {
                // Tìm menu theo ID
                var menu = _context.TbMenus.Find(id);

                if (menu == null) {
                    // Trả về kết quả là false nếu menu không tồn tại
                    return Json(false);
                }

                // Thực hiện xóa menu
                _context.TbMenus.Remove(menu);
                _context.SaveChanges();

                // Trả về kết quả là true để thể hiện rằng xóa thành công
                return Json(true);
            }
            catch (Exception ex) {
                // Xử lý lỗi nếu có
                return Json(false);
            }
        }
        private bool TbMenuExists(int id)
        {
            return (_context.TbMenus?.Any(e => e.MenuId == id)).GetValueOrDefault();
        }
    }
}
