using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NHOM9WEB.Models;
using X.PagedList;

namespace NHOM9WEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogsController : Controller
    {
        private readonly DBNOITHATContext _context;

        public BlogsController(DBNOITHATContext context)
        {
            _context = context;
        }

        // GET: Admin/Blogs
        public async Task<IActionResult> Index(int? page)
        {
            if (page == null) page = 1;
            int pageSize = 3;
            var dBNOITHATContext = _context.TbBlogs.Include(t => t.Account).Include(t => t.Category).ToPagedList((int)page, pageSize);
            return View(dBNOITHATContext);
        }

        // GET: Admin/Blogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TbBlogs == null) {
                return NotFound();
            }

            var tbBlog = await _context.TbBlogs
                .Include(t => t.Account)
                .Include(t => t.Category)
                .FirstOrDefaultAsync(m => m.BlogId == id);
            if (tbBlog == null) {
                return NotFound();
            }

            return View(tbBlog);
        }

        // GET: Admin/Blogs/Create
        public IActionResult Create()
        {
            ViewData["AccountId"] = new SelectList(_context.TbAccounts, "AccountId", "AccountId");
            ViewData["CategoryId"] = new SelectList(_context.TbCategories, "CategoryId", "CategoryId");
            return View();
        }

        // POST: Admin/Blogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BlogId,Title,Alias,CategoryId,Description,Detail,Image,SeoTitle,SeoDescription,SeoKeywords,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy,AccountId,IsActive,MenuId")] TbBlog tbBlog)
        {
            if (ModelState.IsValid) {
                _context.Add(tbBlog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountId"] = new SelectList(_context.TbAccounts, "AccountId", "AccountId", tbBlog.AccountId);
            ViewData["CategoryId"] = new SelectList(_context.TbCategories, "CategoryId", "CategoryId", tbBlog.CategoryId);
            return View(tbBlog);
        }

        // GET: Admin/Blogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TbBlogs == null) {
                return NotFound();
            }

            var tbBlog = await _context.TbBlogs.FindAsync(id);
            if (tbBlog == null) {
                return NotFound();
            }
            ViewData["AccountId"] = new SelectList(_context.TbAccounts, "AccountId", "AccountId", tbBlog.AccountId);
            ViewData["CategoryId"] = new SelectList(_context.TbCategories, "CategoryId", "CategoryId", tbBlog.CategoryId);
            return View(tbBlog);
        }

        // POST: Admin/Blogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BlogId,Title,Alias,CategoryId,Description,Detail,Image,SeoTitle,SeoDescription,SeoKeywords,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy,AccountId,IsActive,MenuId")] TbBlog tbBlog)
        {
            if (id != tbBlog.BlogId) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(tbBlog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException) {
                    if (!TbBlogExists(tbBlog.BlogId)) {
                        return NotFound();
                    }
                    else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountId"] = new SelectList(_context.TbAccounts, "AccountId", "AccountId", tbBlog.AccountId);
            ViewData["CategoryId"] = new SelectList(_context.TbCategories, "CategoryId", "CategoryId", tbBlog.CategoryId);
            return View(tbBlog);
        }
        [HttpPost]
        public IActionResult ToggleIsActive(int id)
        {
            var menu = _context.TbBlogs.Find(id);

            if (menu != null) {
                // Chuyển đổi trạng thái
                menu.IsActive = !menu.IsActive;


                _context.SaveChanges();


                return Json(true);
            }


            return Json(false);
        }
        [HttpPost]
        public IActionResult DeleteBlog(int id)
        {
            try {
                // Tìm menu theo ID
                var blog = _context.TbBlogs.Find(id);

                if (blog == null) {
                    // Trả về kết quả là false nếu menu không tồn tại
                    return Json(false);
                }

                // Thực hiện xóa menu
                _context.TbBlogs.Remove(blog);
                _context.SaveChanges();

                // Trả về kết quả là true để thể hiện rằng xóa thành công
                return Json(true);
            }
            catch (Exception ex) {
                // Xử lý lỗi nếu có
                return Json(false);
            }
        }
        // GET: Admin/Blogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TbBlogs == null) {
                return NotFound();
            }

            var tbBlog = await _context.TbBlogs
                .Include(t => t.Account)
                .Include(t => t.Category)
                .FirstOrDefaultAsync(m => m.BlogId == id);
            if (tbBlog == null) {
                return NotFound();
            }

            return View(tbBlog);
        }

        // POST: Admin/Blogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TbBlogs == null) {
                return Problem("Entity set 'DBNOITHATContext.TbBlogs'  is null.");
            }
            var tbBlog = await _context.TbBlogs.FindAsync(id);
            if (tbBlog != null) {
                _context.TbBlogs.Remove(tbBlog);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbBlogExists(int id)
        {
            return (_context.TbBlogs?.Any(e => e.BlogId == id)).GetValueOrDefault();
        }
    }
}
