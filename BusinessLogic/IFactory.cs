using BusinessLogic.Logging;
using BusinessLogic.Models;
using System.Collections.Generic;

namespace BusinessLogic
{
    public interface IFactory
    {
        IAUser CreateUser(List<string> data, IProcessLogger logger);
        IProcessLogger CreateLogger();
        IUserMethods CreateUserMethods();
        IAState CreateState(List<string> data, IProcessLogger logger);
        IAStore CreateStore(List<string> data, IProcessLogger logger);
        IAItem CreateItem(List<string> data, IProcessLogger logger);
    }
}