using System;
using System.Collections.Generic;

namespace store_appV2_BACKEND.Models
{
    public class Inventory
    {
        //public Inventory()
        //{
        //    TxnSales = new HashSet<TxnSale>();
        //}

        public int Id { get; set; }
        public string ItemName { get; set; } = null!;
        public int ItemPrice { get; set; }
        public string ItemDetails { get; set; } = null!;
        public int Quantity { get; set; }
        public bool IsActive { get; set; }

        //public ICollection<TxnSale> TxnSales { get; set; }
    }
}
