using System;
using System.Collections.Generic;

#nullable disable

namespace Repository.dbModels
{
    public partial class Inventory
    {
        public int StoreNo { get; set; }
        public int PartNo { get; set; }
        public int Quantity { get; set; }

        public virtual Item PartNoNavigation { get; set; }
        public virtual Store StoreNoNavigation { get; set; }
    }
}
