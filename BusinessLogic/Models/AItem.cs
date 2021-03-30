using BusinessLogic.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Models
{
    public class AItem : IAItem
    {
        private readonly IProcessLogger _logger;

        public AItem() { }

        public AItem(List<string> data, IProcessLogger logger)
        {
            PartNo = int.Parse(data[0]);
            PartName = data[1];
            PartDescription = data[2];
            PartPrice = decimal.Parse(data[3]);
            PartSale = decimal.Parse(data[4]);
            PartImage = data[5];
            _logger = logger;
            _logger.Log($"STORE No.: {PartNo} - {PartName.ToUpper()}", true);
        }

        public int PartNo { get; set; }
        public string PartName { get; set; }
        public string PartDescription { get; set; }
        public decimal PartPrice { get; set; }
        public decimal PartSale { get; set; }
        public string PartImage { get; set; }
    }
}
