using System;
using System.Collections.Generic;

#nullable disable

namespace Repository.dbModels
{
    public partial class State
    {
        public State()
        {
            Stores = new HashSet<Store>();
        }

        public int StateNo { get; set; }
        public string StateName { get; set; }
        public string StateCode { get; set; }
        public decimal TaxRate { get; set; }

        public virtual ICollection<Store> Stores { get; set; }
    }
}
