using System;
using System.Collections.Generic;

#nullable disable

namespace Repository.dbModels
{
    public partial class OrderHistory
    {
        public int OrderNo { get; set; }
        public int PartNo { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }

        public virtual Order OrderNoNavigation { get; set; }
        public virtual Item PartNoNavigation { get; set; }
    }
}
