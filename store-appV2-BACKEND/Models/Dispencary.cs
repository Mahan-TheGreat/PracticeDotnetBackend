using System;
using System.Collections.Generic;

namespace store_appV2_BACKEND.Models
{
    public partial class Dispencary
    {
        public int Id { get; set; }
        public string DispencaryCode { get; set; } = null!;
        public string DispencaryName { get; set; } = null!;
        public string? DispencaryLocation { get; set; }
        public bool IsActive { get; set; }
    }
}
