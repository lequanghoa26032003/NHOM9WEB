using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using NHOM9WEB.Models;
using NHOM9WEB.Models.ViewModel;
using NHOM9WEB.Repository;
using NHOM9WEB.Utilities;

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
            TempData["success"]="Thêm thành công";
            return Redirect(Request.Headers["Referer"].ToString());
        }
        public async Task<IActionResult> Decrease(int Id)
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");
            CartItem cartItems = cart.Where(c => c.ProductId==Id).FirstOrDefault();
            if (cartItems.Quantity > 1) {
                --cartItems.Quantity;
            }
            else {
                cart.RemoveAll(c => c.ProductId==Id);
            }
            if (cart.Count==0) {
                HttpContext.Session.Remove("Cart");
            }
            else {
                HttpContext.Session.SetJson("Cart", cart);
            }
            TempData["success"]="Giảm số lượng thành công";

            return RedirectToRoute("Index", new { slug = "your-slug", id = 3 });

        }
        public async Task<IActionResult> Increase(int Id)
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");
            CartItem cartItems = cart.Where(c => c.ProductId==Id).FirstOrDefault();
            if (cartItems.Quantity>=1) {
                ++cartItems.Quantity;
            }
            else {
                cart.RemoveAll(c => c.ProductId==Id);

            }
            if (cart.Count==0) {
                HttpContext.Session.Remove("Cart");
            }
            else {
                HttpContext.Session.SetJson("Cart", cart);

            }
            TempData["success"]="Tăng số lượng sản phẩm thành công";

            return RedirectToRoute("Index", new { slug = "your-slug", id = 3 });
        }
        public async Task<IActionResult> Remove(int Id, bool? confirmDelete)
        {
            if (confirmDelete.HasValue && confirmDelete.Value) {
                List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");


                var itemToRemove = cart.FirstOrDefault(item => item.ProductId == Id);
                if (itemToRemove != null) {
                    cart.Remove(itemToRemove);
                    HttpContext.Session.SetJson("Cart", cart);
                }
                TempData["success"]="Xóa sản phẩm thành công";

                return RedirectToAction("Index", new { slug = "your-slug", id = 3 });
            }
            else {
                return RedirectToAction("Index", new { slug = "your-slug", id = 3 });
            }
        }
        [Route("/checkout")]
        public IActionResult CheckOut()
        {
            if (Functions._AccountId==null ||Functions._AccountId==0) {
                return Redirect("login-dang-nhap-8.html");

            }
            else {
                List<CartItem> cartItems = HttpContext.Session.GetJson<List<CartItem>>("Cart")??new List<CartItem>();
                CartItemViewModel cartVM = new()
                {
                    CartItems=cartItems,
                    GrandTotal=cartItems.Sum(x => x.Quantity*x.Price)
                };
                return View(cartVM);

            }

        }
        [HttpPost]
        public IActionResult Create(int? id, string name, string phone, string email, string message)
        {
            try {
                TbProductReview comment = new TbProductReview();
                comment.ProductId = id;
                comment.Name = name;
                comment.Phone = phone;
                comment.Email = email;
                comment.Detail = message;
                comment.CreatedDate = DateTime.Now;
                comment.IsActive = true;
                _context.Add(comment);
                _context.SaveChangesAsync();
                return Json(new { status = true });
            }
            catch {
                return Json(new { status = false });
            }
        }
        public bool Order(string name, string phone, string address)
        {
            // Xử lý khi đặt hàng thành công
            try {
                var cart = GetCartItems();
                if (cart.IsNullOrEmpty()) {
                    return false;
                }
                else {
                    int totalAmount = 0;

                    var order = new TbOrder();
                    order.CustomerName = name;
                    order.Phone = phone;
                    order.Address = address;
                    order.TotalAmount = totalAmount;
                    order.OrderStatusId = 1;
                    order.CreatedDate = DateTime.Now;
                    _context.TbOrders.Add(order);
                    _context.SaveChanges();
                    int orderId = order.OrderId;
                    foreach (var item in cart) {
                        var orderDetail = new TbOrderDetail();
                        orderDetail.OrderId = orderId;
                        orderDetail.ProductId=item.product.ProductId;
                        orderDetail.Price = item.product.Price;
                        orderDetail.Quantity = item.Quantity;
                        _context.TbOrderDetails.Add(orderDetail);
                        _context.SaveChanges();
                    }

                }
                ClearCart();
                return true;
            }
            catch {
                return false;
            }
        }

        public const string CARTKEY = "cart";

        List<CartItem> GetCartItems()
        {
            List<CartItem> cartItems = HttpContext.Session.GetJson<List<CartItem>>("Cart")??new List<CartItem>();
            CartItemViewModel cartVM = new()
            {
                CartItems=cartItems,
                GrandTotal=cartItems.Sum(x => x.Quantity*x.Price)
            };
            return cartItems;
        }
        void ClearCart()
        {
            var session = HttpContext.Session;
            session.Remove(CARTKEY);
        }

        // Lưu Cart (Danh sách CartItem) vào session
        void SaveCartSession(List<CartItem> ls)
        {
            var session = HttpContext.Session;
            string jsoncart = JsonConvert.SerializeObject(ls);
            session.SetString(CARTKEY, jsoncart);
        }
    }
}
