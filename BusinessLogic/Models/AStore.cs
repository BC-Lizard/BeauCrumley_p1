using BusinessLogic.Logging;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Models
{
    public class AStore : IAStore
    {
        private readonly IProcessLogger _logger;

        public AStore() { }

        public AStore(List<string> data, IProcessLogger logger)
        {
            StoreNo = int.Parse(data[0]);
            StoreName = data[1];
            StoreCity = data[2];
            StoreStateId = int.Parse(data[3]);
            StoreZipCode = int.Parse(data[4]);
            StoreStreetAddress = data[5];
            _logger = logger;
            _logger.Log($"STORE No.: {StoreNo} - {StoreName.ToUpper()}", true);
        }
        public int StoreNo { get; set; }
        public string StoreName { get; set; }
        public string StoreCity { get; set; }
        public int StoreStateId { get; set; }
        public int StoreZipCode { get; set; }
        public string StoreStreetAddress { get; set; }

        public IAState StoreState { get; set; }

        public List<IAItem> Inventory { get; set; }
        public List<int> InvLevels { get; set; }
    }
}
