using System;
using System.Collections.Generic;

namespace NHOM9WEB.Models;

public partial class TbOrderManagement
{
    public int OrderId { get; set; }

    public string? CustomerName { get; set; }

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public int? TotalAmount { get; set; }

    public int? OrderStatusId { get; set; }

    public decimal? Price { get; set; }

    public string? Title { get; set; }

    public int? Quantity { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string? ModifiedBy { get; set; }
}
