using System;
using System.Collections.Generic;

namespace store_appV2_BACKEND.Models
{
    public partial class UserCred
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Email { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Pass { get; set; } = null!;

    }
}
