using System;
using System.Collections.Generic;

namespace store_appV2_BACKEND.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Contact { get; set; }
        public string? Location { get; set; }
        public bool IsActive { get; set; }
    }
}
