using Microsoft.AspNetCore.Mvc;
using NHOM9WEB.Models;
using NHOM9WEB.Utilities;

namespace NHOM9WEB.Controllers
{
    public class LoginController : Controller
    {
        private readonly DBNOITHATContext _context;

        public LoginController(DBNOITHATContext context)
        {
            _context = context;
        }
        [Route("/login-{slug}-{id:int}.html", Name = "Login")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost("/Login")]
        public IActionResult Index(TbAccount account)
        {
            if (account == null) {
                return NotFound();
            }
            string password = HashMD5.GetHash(account.Password);
            var check = _context.TbAccounts.Where(m => m.Username == account.Username && m.Password == password).FirstOrDefault();
            if (check == null) {
                Functions._Message = "Invalid Username or Password";
                return Redirect("login-dang-nhap-8.html");
            }
            Functions._Message = string.Empty;
            Functions._AccountId = check.AccountId;
            Functions._Username = check.Username;
            return Redirect("index-trang-chu-1.html");
        }
    }
}
