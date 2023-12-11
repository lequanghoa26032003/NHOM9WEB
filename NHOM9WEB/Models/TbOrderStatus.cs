﻿using System;
using System.Collections.Generic;

namespace NHOM9WEB.Models
{
    public partial class TbOrderStatus
    {
        public TbOrderStatus()
        {
            TbOrders = new HashSet<TbOrder>();
        }

        public int OrderStatusId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<TbOrder> TbOrders { get; set; }
    }
}