using System;
using System.Collections.Generic;

namespace NHOM9WEB.Models
{
    public partial class ShopCategory
    {
        public int Expr1 { get; set; }
        public string? Expr11 { get; set; }
        public int CategoryProductId { get; set; }
        public string? Type { get; set; }
        public string? Alias { get; set; }
        public string? Description { get; set; }
        public string? Icon { get; set; }
        public int? Position { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public bool IsActive { get; set; }
        public int? Expr2 { get; set; }
        public string? Title { get; set; }
        public string? Image { get; set; }
        public int? PriceSale { get; set; }
        public bool IsBestSeller { get; set; }
    }
}
