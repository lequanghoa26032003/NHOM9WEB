using System;
using System.Collections.Generic;

namespace NHOM9WEB.Models
{
    public partial class TbAccount
    {
        public TbAccount()
        {
            TbBlogs = new HashSet<TbBlog>();
        }

        public int AccountId { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? FullName { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public int? RoleId { get; set; }
        public string? LastLogin { get; set; }
        public bool IsActive { get; set; }

        public virtual TbRole? Role { get; set; }
        public virtual ICollection<TbBlog> TbBlogs { get; set; }
    }
}
