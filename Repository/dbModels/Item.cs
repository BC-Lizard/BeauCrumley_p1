using System;
using System.Collections.Generic;

#nullable disable

namespace Repository.dbModels
{
    public partial class Item
    {
        public Item()
        {
            Inventories = new HashSet<Inventory>();
            OrderHistories = new HashSet<OrderHistory>();
        }

        public int PartNo { get; set; }
        public string PartName { get; set; }
        public string PartDescription { get; set; }
        public decimal PartPrice { get; set; }
        public decimal PartSale { get; set; }
        public string PartImage { get; set; }

        public virtual ICollection<Inventory> Inventories { get; set; }
        public virtual ICollection<OrderHistory> OrderHistories { get; set; }
    }
}
