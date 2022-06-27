using System;
using System.Collections.Generic;

namespace store_appV2_BACKEND.Models
{
    public class TxnSale
    {

        public int Id { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public int TotalPrice { get; set; }

    }
}
