using Microsoft.AspNetCore.Mvc;
using NHOM9WEB.Models;
using System.Diagnostics;
using X.PagedList;

namespace NHOM9WEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DBNOITHATContext _context;

        public HomeController(ILogger<HomeController> logger, DBNOITHATContext context)
        {
            _logger = logger;
            _context=context;
        }
        public IActionResult Index(int? page)
        {
            if (page == null) page = 1;
            int pageSize = 8;
            var list = _context.TbProducts.ToPagedList((int)page, pageSize);
            return View(list);
        }
        [Route("/index-{slug}-{id:int}.html", Name = "Home")]
        public IActionResult Home(int? id, int? page)
        {
            if (page == null) page = 1;
            int pageSize = 8;
            if (id==null) {
                return NotFound();
            }
            var list = _context.TbProducts.ToPagedList((int)page, pageSize);
            if (list==null) {
                return NotFound();
            }
            return View(list);
        }
        [Route("/shop-{slug}-{id:int}.html", Name = "Sanpham")]
        public IActionResult SanPham(int? id, int? page)
        {
            if (page == null) page = 1;
            int pageSize = 6;
            if (id==null) {
                return NotFound();
            }
            var list = _context.TbProducts.ToPagedList((int)page, pageSize);
            if (list==null) {
                return NotFound();
            }
            return View(list);
        }
        [Route("/review-{slug}-{id:int}.html", Name = "ProductDetails")]
        public IActionResult ProductDetails(int? id)
        {
            var list = _context.TbReviews
                 .FirstOrDefault(m => (m.ProductId==id)&&(m.IsActive==true));
            return View(list);
        }

        public IActionResult SanPhamTheoLoai(int maloai, int? page)
        {
            if (page == null) page = 1;
            int pageSize = 8;
            var list = _context.ShopCategories.Where(m => m.CategoryProductId==maloai).OrderBy(x => x.Title).ToPagedList((int)page, pageSize);

            return View(list);
        }
        public IActionResult Privacy()
        {
            return View();
        }
        [Route("/post-{slug}-{id:int}.html", Name = "Details")]
        public IActionResult Details(int? id, int? page)
        {
            if (page == null) page = 1;
            int pageSize = 4;
            if (id==null) {
                return NotFound();
            }
            var post = _context.TbBlogs.FirstOrDefault(m => (m.BlogId==id)&&(m.IsActive==true));
            ViewBag.blogComment = _context.TbBlogComments.ToList();
            if (post==null) {
                return NotFound();

            }

            return View(post);
        }
        [Route("/List-{slug}-{id:int}.html", Name = "List")]
        public IActionResult List(int? id, int? page)
        {
            if (page == null) page = 1;
            int pageSize = 4;
            if (id==null) {
                return NotFound();
            }
            var list = _context.TbviewBlogs
                .Where(m => (m.MenuId==id)&&(m.IsActive==true)).ToPagedList((int)page, pageSize);
            if (list==null) {
                return NotFound();
            }
            return View(list);
        }
        [Route("/contact-{slug}-{id:int}.html", Name = "Contact")]
        public IActionResult Contact(int? id, int? page)
        {
            return View();

        }
        public IActionResult Search(string keyword, int? page)
        {
            if (page == null) page = 1;
            int pageSize = 4;
            // Assuming you want to search for blog posts based on the keyword
            var searchResults = _context.TbviewBlogs
                .Where(blog => blog.Title.Contains(keyword) || blog.Description.Contains(keyword)).ToPagedList((int)page, pageSize);

            // You can pass the search results to your view
            return View(searchResults);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}