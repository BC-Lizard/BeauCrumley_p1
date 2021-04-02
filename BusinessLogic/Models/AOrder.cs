using BusinessLogic.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Models
{
    public class AOrder : IAOrder
    {
        private readonly IProcessLogger _logger;
        public AOrder() { }

        public AOrder(List<string> data, IProcessLogger logger)
        {
            OrderNo = int.Parse(data[0]);
            StoreNo = int.Parse(data[1]);
            AccountNo = int.Parse(data[2]);
            OrderDate = createNewDate(data[3]);
            Subtotal = decimal.Parse(data[4]);
            Tax = decimal.Parse(data[5]);
            Total = decimal.Parse(data[6]);
            _logger = logger;
            _logger.Log($"ORDER: {OrderNo} of total {Total}", true);
        }

        public int OrderNo { get; set; }
        public int StoreNo { get; set; }
        public int AccountNo { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }

        private DateTime createNewDate(string dateData)
        {
            DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(long.Parse(dateData));
            DateTime date = dateTimeOffset.DateTime;
            //date = DateTime.Parse(dateData);
            return date;
        }
    }
}
