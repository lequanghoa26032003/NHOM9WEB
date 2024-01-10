using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NHOM9WEB.Models;
using X.PagedList;

namespace NHOM9WEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly DBNOITHATContext _context;

        public ProductsController(DBNOITHATContext context)
        {
            _context = context;
        }

        // GET: Admin/Products
        public async Task<IActionResult> Index(int? page)
        {
            if (page == null) page = 1;
            int pageSize = 4;
            var dBNOITHATContext = _context.TbProducts.Include(t => t.CategoryProduct).ToPagedList((int)page, pageSize);
            return View(dBNOITHATContext);
        }

        // GET: Admin/Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TbProducts == null) {
                return NotFound();
            }

            var tbProduct = await _context.TbProducts
                .Include(t => t.CategoryProduct)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (tbProduct == null) {
                return NotFound();
            }

            return View(tbProduct);
        }

        // GET: Admin/Products/Create
        public IActionResult Create()
        {
            ViewData["CategoryProductId"] = new SelectList(_context.TbProductCategories, "CategoryProductId", "CategoryProductId");
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,Title,Alias,CategoryProductId,Description,Detail,Image,Price,PriceSale,Quantity,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy,IsNew,IsBestSeller,UnitInStock,IsActive")] TbProduct tbProduct)
        {
            if (ModelState.IsValid) {
                _context.Add(tbProduct);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryProductId"] = new SelectList(_context.TbProductCategories, "CategoryProductId", "CategoryProductId", tbProduct.CategoryProductId);
            return View(tbProduct);
        }

        // GET: Admin/Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TbProducts == null) {
                return NotFound();
            }

            var tbProduct = await _context.TbProducts.FindAsync(id);
            if (tbProduct == null) {
                return NotFound();
            }
            ViewData["CategoryProductId"] = new SelectList(_context.TbProductCategories, "CategoryProductId", "CategoryProductId", tbProduct.CategoryProductId);
            return View(tbProduct);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,Title,Alias,CategoryProductId,Description,Detail,Image,Price,PriceSale,Quantity,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy,IsNew,IsBestSeller,UnitInStock,IsActive")] TbProduct tbProduct)
        {
            if (id != tbProduct.ProductId) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(tbProduct);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException) {
                    if (!TbProductExists(tbProduct.ProductId)) {
                        return NotFound();
                    }
                    else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryProductId"] = new SelectList(_context.TbProductCategories, "CategoryProductId", "CategoryProductId", tbProduct.CategoryProductId);
            return View(tbProduct);
        }

        // GET: Admin/Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TbProducts == null) {
                return NotFound();
            }

            var tbProduct = await _context.TbProducts
                .Include(t => t.CategoryProduct)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (tbProduct == null) {
                return NotFound();
            }

            return View(tbProduct);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try {
                if (_context.TbProducts == null) {
                    throw new InvalidOperationException("Context is null.");
                }

                var tbCategory = _context.TbProducts.Find(id);
                if (tbCategory != null) {
                    _context.TbProducts.Remove(tbCategory);
                    await _context.SaveChangesAsync();
                }
                else {
                    throw new InvalidOperationException($"sản phẩm có ID tương ứng với giá trị {id} không được tìm thấy.");
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex) {
                return RedirectToAction("Error");
            }
        }


        [HttpPost]
        public IActionResult ToggleIsActive(int id)
        {
            var sp = _context.TbProducts.Find(id);

            if (sp != null) {
                // Chuyển đổi trạng thái
                sp.IsActive = !sp.IsActive;


                _context.SaveChanges();


                return Json(true);
            }


            return Json(false);
        }
        [HttpPost]
        public IActionResult Deletesp(int id)
        {
            try {
                // Tìm menu theo ID
                var sp = _context.TbProducts.Find(id);

                if (sp == null) {
                    // Trả về kết quả là false nếu menu không tồn tại
                    return Json(false);
                }

                // Thực hiện xóa menu
                _context.TbProducts.Remove(sp);
                _context.SaveChanges();

                // Trả về kết quả là true để thể hiện rằng xóa thành công
                return Json(true);
            }
            catch (Exception ex) {
                // Xử lý lỗi nếu có
                return Json(false);
            }
        }
        private bool TbProductExists(int id)
        {
            return (_context.TbProducts?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }


    }
}
