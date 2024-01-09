using Microsoft.AspNetCore.Mvc;
using NHOM9WEB.Models;
using NHOM9WEB.Utilities;
namespace NHOM9WEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly DBNOITHATContext _context;

        public HomeController(DBNOITHATContext context)
        {
            _context = context;
        }
        public IActionResult Index(TbAccount account)
        {

            if (!Functions.IsLogin()) {
                return Redirect("login-dang-nhap-8.html");

            }
            return View();

        }
    }
}
