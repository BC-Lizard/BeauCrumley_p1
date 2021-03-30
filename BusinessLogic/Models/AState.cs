using BusinessLogic.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Models
{
    public class AState : IAState
    {
        private readonly IProcessLogger _logger;
        public AState() { }

        public AState(List<string> data, IProcessLogger logger)
        {
            StateNo = int.Parse(data[0]);
            StateName = data[1];
            StateCode = data[2].ToUpper();
            TaxRate = decimal.Parse(data[3]);
            _logger = logger;
            _logger.Log($"STATE: {StateName.ToUpper()} - {StateCode}", true);
        }

        public int StateNo { get; set; }
        public string StateName { get; set; }
        public string StateCode { get; set; }
        public decimal TaxRate { get; set; }
    }
}
