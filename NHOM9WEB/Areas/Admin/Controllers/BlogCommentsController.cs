using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NHOM9WEB.Models;
using X.PagedList;

namespace NHOM9WEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogCommentsController : Controller
    {
        private readonly DBNOITHATContext _context;

        public BlogCommentsController(DBNOITHATContext context)
        {
            _context = context;
        }

        // GET: Admin/BlogComments
        public async Task<IActionResult> Index(int? page)
        {
            if (page == null) page = 1;
            int pageSize = 3;
            var dBNOITHATContext = _context.TbBlogComments.Include(t => t.Blog).ToPagedList((int)page, pageSize);
            return View(dBNOITHATContext);
        }

        // GET: Admin/BlogComments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TbBlogComments == null) {
                return NotFound();
            }

            var tbBlogComment = await _context.TbBlogComments
                .Include(t => t.Blog)
                .FirstOrDefaultAsync(m => m.CommentId == id);
            if (tbBlogComment == null) {
                return NotFound();
            }

            return View(tbBlogComment);
        }

        // GET: Admin/BlogComments/Create
        public IActionResult Create()
        {
            ViewData["BlogId"] = new SelectList(_context.TbBlogs, "BlogId", "BlogId");
            return View();
        }

        // POST: Admin/BlogComments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CommentId,Name,Phone,Email,CreatedDate,Detail,BlogId,IsActive")] TbBlogComment tbBlogComment)
        {
            if (ModelState.IsValid) {
                _context.Add(tbBlogComment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BlogId"] = new SelectList(_context.TbBlogs, "BlogId", "BlogId", tbBlogComment.BlogId);
            return View(tbBlogComment);
        }

        // GET: Admin/BlogComments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TbBlogComments == null) {
                return NotFound();
            }

            var tbBlogComment = await _context.TbBlogComments.FindAsync(id);
            if (tbBlogComment == null) {
                return NotFound();
            }
            ViewData["BlogId"] = new SelectList(_context.TbBlogs, "BlogId", "BlogId", tbBlogComment.BlogId);
            return View(tbBlogComment);
        }

        // POST: Admin/BlogComments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CommentId,Name,Phone,Email,CreatedDate,Detail,BlogId,IsActive")] TbBlogComment tbBlogComment)
        {
            if (id != tbBlogComment.CommentId) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(tbBlogComment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException) {
                    if (!TbBlogCommentExists(tbBlogComment.CommentId)) {
                        return NotFound();
                    }
                    else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BlogId"] = new SelectList(_context.TbBlogs, "BlogId", "BlogId", tbBlogComment.BlogId);
            return View(tbBlogComment);
        }

        // GET: Admin/BlogComments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TbBlogComments == null) {
                return NotFound();
            }

            var tbBlogComment = await _context.TbBlogComments
                .Include(t => t.Blog)
                .FirstOrDefaultAsync(m => m.CommentId == id);
            if (tbBlogComment == null) {
                return NotFound();
            }

            return View(tbBlogComment);
        }

        // POST: Admin/BlogComments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TbBlogComments == null) {
                return Problem("Entity set 'DBNOITHATContext.TbBlogComments'  is null.");
            }
            var tbBlogComment = await _context.TbBlogComments.FindAsync(id);
            if (tbBlogComment != null) {
                _context.TbBlogComments.Remove(tbBlogComment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbBlogCommentExists(int id)
        {
            return (_context.TbBlogComments?.Any(e => e.CommentId == id)).GetValueOrDefault();
        }
    }
}
