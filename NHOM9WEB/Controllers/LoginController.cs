using Microsoft.AspNetCore.Mvc;

namespace NHOM9WEB.Controllers
{
    public class LoginController : Controller
    {
        [Route("/login-{slug}-{id:int}.html", Name = "Login")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
