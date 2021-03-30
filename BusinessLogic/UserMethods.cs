using BusinessLogic.Models;
using BusinessLogic.Logging;
using System;
using System.Collections.Generic;
using Repository;

namespace BusinessLogic
{
    public class UserMethods : IUserMethods
    {
        private readonly IFactory _factory;
        private readonly IDataFetcher _repoDataFetcher;
        public UserMethods() { }
        public UserMethods(IFactory factory, IDataFetcher repoDataFetcher)
        {
            _factory = factory;
            _repoDataFetcher = repoDataFetcher;
        }
        public List<IAUser> GetUsers()
        {
            var users = new List<IAUser>();
            var data = _repoDataFetcher.RepoGetUserData();
            foreach (var user in data)
            {
                users.Add(_factory.CreateUser(user, _factory.CreateLogger()));
            }
            return users;
        }
        public List<IAUser> GetUsers(string email)
        {
            var users = new List<IAUser>();
            var data = _repoDataFetcher.RepoGetUserData(email);
            foreach (var user in data)
            {
                users.Add(_factory.CreateUser(user, _factory.CreateLogger()));
            }
            return users;
        }

        public bool IsUser(string email)
        {
            return _repoDataFetcher.RepoIsUser(email);
        }

        public IAUser BlankUser()
        {
            var blankData = new List<string>() {"0", "_", "_", "_", "_", "_", "0", "_", "0", "0"};
            return _factory.CreateUser(blankData, _factory.CreateLogger());
        }

        public List<IAStore> GetStores()
        {
            var stores = new List<IAStore>();
            var data = _repoDataFetcher.RepoGetStoreData();
            IAStore newStore;
            foreach (var store in data)
            {
                newStore = _factory.CreateStore(store, _factory.CreateLogger());
                InitStoreState(newStore);
                InitStoreInventory(newStore);
                stores.Add(newStore);
            }
            return stores;
        }

        private void InitStoreState(IAStore store)
        {
            store.StoreState = GetState(store.StoreStateId);
        }
        private void InitStoreInventory(IAStore store)
        {
            var invData = _repoDataFetcher.RepoGetInvDataForStore(store.StoreNo);
            var newInv = new List<IAItem>();
            var newInvLevels = new List<int>();
            foreach (var part in invData)
            {
                newInv.Add(GetItem(part[0]));
                newInvLevels.Add(part[1]);
            }
            store.Inventory = newInv;
            store.InvLevels = newInvLevels;
        }

        public IAState GetState(int Id)
        {
            var data = _repoDataFetcher.RepoGetStateData(Id);
            var state = _factory.CreateState(data[0], _factory.CreateLogger());
            return state;
        }

        public IAItem GetItem(int Id)
        {
            return _factory.CreateItem(_repoDataFetcher.RepoGetItemData(Id)[0], _factory.CreateLogger());
        }

        public List<IAItem> GetAllItems()
        {
            var items = new List<IAItem>();
            var data = _repoDataFetcher.RepoGetItemData();
            foreach (var part in data)
            {
                items.Add(_factory.CreateItem(part, _factory.CreateLogger()));
            }
            return items;
        }
    }
}