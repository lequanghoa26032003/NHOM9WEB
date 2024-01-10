using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NHOM9WEB.Models;
namespace NHOM9WEB.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class OrdersController : Controller
    {
        private readonly DBNOITHATContext _context;

        public OrdersController(DBNOITHATContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return _context.TbMenus != null ?
                        View(await _context.TbOrderManagements.ToListAsync()) :
                        Problem("Entity set 'DBNOITHATContext.TbOrderManagements'  is null.");
        }
        [HttpPost]
        public IActionResult DeleteOrder(int id)
        {
            try {
                // Tìm đơn hàng theo ID
                var order = _context.TbOrders.FirstOrDefault(o => o.OrderId == id);

                if (order == null) {
                    // Trả về kết quả là false nếu đơn hàng không tồn tại
                    return Json(false);
                }

                // Tìm và xóa các chi tiết đơn hàng tương ứng
                var orderDetails = _context.TbOrderDetails.Where(od => od.OrderId == id).ToList();
                _context.TbOrderDetails.RemoveRange(orderDetails);

                // Giảm số lượng hàng trong bảng TbProduct
                foreach (var orderDetail in orderDetails) {
                    var product = _context.TbProducts.FirstOrDefault(p => p.ProductId == orderDetail.ProductId);

                    if (product != null) {
                        // Giảm số lượng hàng dựa trên thông tin chi tiết đơn hàng
                        product.Quantity -= orderDetail.Quantity;
                    }
                }

                // Thực hiện xóa đơn hàng
                _context.TbOrders.Remove(order);
                _context.SaveChanges();

                // Trả về kết quả là true để thể hiện rằng xóa thành công
                return Json(true);
            }
            catch (Exception ex) {
                // Xử lý lỗi nếu có
                return Json(false);
            }
        }


    }
}
