using System;
using System.Collections.Generic;

#nullable disable

namespace Repository.dbModels
{
    public partial class User
    {
        public User()
        {
            Orders = new HashSet<Order>();
        }

        public int AccountNo { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
        public string PasswordSalt { get; set; }
        public string PasswordHash { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int Permission { get; set; }
        public int? DefaultStore { get; set; }

        public virtual Store DefaultStoreNavigation { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
