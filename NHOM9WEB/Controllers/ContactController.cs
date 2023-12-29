using Microsoft.AspNetCore.Mvc;
using NHOM9WEB.Models;

namespace NHOM9WEB.Controllers
{
    public class ContactController : Controller
    {
        private readonly DBNOITHATContext _context;
        public ContactController(DBNOITHATContext context)
        {
            _context = context;
        }
        [Route("/contact-{slug}-{id:int}.html", Name = "Contact")]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(string name, string phone, string email, string message)
        {
            try {
                TbContact contact = new TbContact();
                contact.Name = name;
                contact.Phone = phone;
                contact.Email = email;
                contact.Message = message;
                contact.CreatedDate = DateTime.Now;
                _context.Add(contact);
                _context.SaveChangesAsync();
                return Json(new { status = true });
            }
            catch {
                return Json(new { status = false });
            }
        }
    }
}
