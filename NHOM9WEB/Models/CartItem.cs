namespace NHOM9WEB.Models
{
    public class CartItem
    {
        public long ProductId { get; set; }
        public int Quantity { get; set; }
        public string ProductName { get; set; }
        public int? Price { get; set; }
        public string Image { get; set; }
        public TbProduct product { set; get; }
        public int? Total
        {
            get
            {
                return Quantity*Price;
            }
        }
        public CartItem()
        {

        }
        public CartItem(TbProduct product)
        {
            ProductId=product.ProductId;
            ProductName=product.Title;
            Price=product.Price;
            Quantity=1;
            Image=product.Image;
        }
    }
}
