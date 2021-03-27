using System;
using System.Collections.Generic;

#nullable disable

namespace Repository.dbModels
{
    public partial class Order
    {
        public Order()
        {
            OrderHistories = new HashSet<OrderHistory>();
        }

        public int OrderNo { get; set; }
        public int? StoreNo { get; set; }
        public int? AccountNo { get; set; }
        public DateTime? OrderDate { get; set; }
        public decimal? Subtotal { get; set; }
        public decimal? Tax { get; set; }
        public decimal? Total { get; set; }

        public virtual User AccountNoNavigation { get; set; }
        public virtual Store StoreNoNavigation { get; set; }
        public virtual ICollection<OrderHistory> OrderHistories { get; set; }
    }
}
