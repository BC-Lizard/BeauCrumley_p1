using BusinessLogic.Models;
using System.Collections.Generic;

namespace BusinessLogic
{
    public interface IUserMethods
    {
        List<IAUser> GetUsers();
        List<IAUser> GetUsers(string email);
        bool IsUser(string email);
        bool registerNewUser(string userData);
        IAUser BlankUser();
        IAState GetState(int Id);
        List<IAStore> GetStores();
        List<IAItem> GetAllItems();
    }
}