using System.Collections.Generic;

namespace BusinessLogic.Models
{
    public interface IAStore
    {
        string StoreCity { get; set; }
        string StoreName { get; set; }
        int StoreNo { get; set; }
        int StoreStateId { get; set; }
        string StoreStreetAddress { get; set; }
        int StoreZipCode { get; set; }

        IAState StoreState { get; set; }

        List<IAItem> Inventory { get; set; }
        List<int> InvLevels { get; set; }
    }
}