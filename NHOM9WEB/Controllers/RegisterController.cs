using Microsoft.AspNetCore.Mvc;
using NHOM9WEB.Models;
using NHOM9WEB.Utilities;


namespace NHOM9WEB.Controllers
{
    public class RegisterAdminController : Controller
    {
        private readonly DBNOITHATContext _context;

        public RegisterAdminController(DBNOITHATContext context)
        {
            _context = context;
        }
        [HttpGet("/Register")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost("/Register")]
        public IActionResult Create(TbAccount account)
        {
            if (account == null) { return NotFound(); }

            var check = _context.TbAccounts.Where(m => m.Username == account.Username).FirstOrDefault();
            if (check != null) {
                Functions._Message = "Trùng tài khoản";
                return RedirectToAction("Index", "Register");
            }
            Functions._Message = string.Empty;
            account.Password = HashMD5.GetHash(account.Password != null ? account.Password : "");
            _context.Add(account);
            _context.SaveChanges();
            return Redirect("login-dang-nhap-8.html");
        }
    }
}
