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
        IAStore GetStores(int Id);
        IAItem GetItem(int Id);
        List<IAItem> GetAllItems();
        bool saveNewOrder(string orderData, string itemData);
    }
}