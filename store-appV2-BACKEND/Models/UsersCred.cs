using System;
using System.Collections.Generic;

namespace store_appV2_BACKEND.Models
{
    public partial class UsersCred
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Password { get; set; }
        public string? Location { get; set; }
        public bool IsActive { get; set; }
    }
}
