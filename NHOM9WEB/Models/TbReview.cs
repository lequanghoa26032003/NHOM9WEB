using System;
using System.Collections.Generic;

namespace NHOM9WEB.Models
{
    public partial class TbReview
    {
        public int ProductReviewId { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? Detail { get; set; }
        public int? ProductId { get; set; }
        public bool IsActive { get; set; }
        public string? Image { get; set; }
        public int? Price { get; set; }
        public int? PriceSale { get; set; }
        public bool IsBestSeller { get; set; }
        public string? Title { get; set; }
    }
}
