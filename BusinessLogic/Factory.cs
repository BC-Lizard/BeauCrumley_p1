using BusinessLogic.Logging;
using BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class Factory : IFactory
    {
        public IAUser CreateUser(List<string> data, IProcessLogger logger)
        {
            return new AUser(data, logger);
        }

        public IProcessLogger CreateLogger()
        {
            return new ConsoleLogger();
        }

        public IAState CreateState(List<string> data, IProcessLogger logger)
        {
            return new AState(data, logger);
        }

        public IAStore CreateStore(List<string> data, IProcessLogger logger)
        {
            return new AStore(data, logger);
        }

        public IAItem CreateItem(List<string> data, IProcessLogger logger)
        {
            return new AItem(data, logger);
        }

        public IAOrder CreateOrder(List<string> data, IProcessLogger logger)
        {
            return new AOrder(data, logger);
        }
    }
}
