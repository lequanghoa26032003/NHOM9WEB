using System;
using System.Collections.Generic;

namespace NHOM9WEB.Models;

public partial class TbSubscribe
{
    public int SubscribeId { get; set; }

    public string? Email { get; set; }

    public DateTime? CreateDate { get; set; }
}
