using System;
using System.Collections.Generic;

#nullable disable

namespace Repository.dbModels
{
    public partial class Store
    {
        public Store()
        {
            Inventories = new HashSet<Inventory>();
            Orders = new HashSet<Order>();
            Users = new HashSet<User>();
        }

        public int StoreNo { get; set; }
        public string StoreName { get; set; }
        public string StoreCity { get; set; }
        public int? StoreState { get; set; }
        public int StoreZipCode { get; set; }
        public string StoreStreetAddress { get; set; }

        public virtual State StoreStateNavigation { get; set; }
        public virtual ICollection<Inventory> Inventories { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
