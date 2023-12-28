using Microsoft.AspNetCore.Mvc;
using NHOM9WEB.Models;
using NHOM9WEB.Models.ViewModel;
using NHOM9WEB.Repository;

namespace NHOM9WEB.Controllers
{
    public class CartController : Controller
    {
        private readonly ILogger<CartController> _logger;
        private readonly DBNOITHATContext _context;

        public CartController(ILogger<CartController> logger, DBNOITHATContext context)
        {
            _logger = logger;
            _context=context;
        }
        //[Route("/page-{slug}-{id:int}.html", Name = "Cart")]
        //public IActionResult Cart()
        //{
        //    return View();
        //}

        [Route("/cart-{slug}-{id:int}.html", Name = "Index")]
        public IActionResult Index()
        {
            List<CartItem> cartItems = HttpContext.Session.GetJson<List<CartItem>>("Cart")??new List<CartItem>();
            CartItemViewModel cartVM = new()
            {
                CartItems=cartItems,
                GrandTotal=cartItems.Sum(x => x.Quantity*x.Price)
            };
            return View(cartVM);
        }
        public async Task<IActionResult> Add(int Id)
        {
            TbProduct product = await _context.TbProducts.FindAsync(Id);
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart")??new List<CartItem>();
            CartItem cartItems = cart.Where(c => c.ProductId==Id).FirstOrDefault();
            if (cartItems==null) {
                cart.Add(new CartItem(product));
            }
            else {
                cartItems.Quantity +=1;
            }
            HttpContext.Session.SetJson("Cart", cart);
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}
