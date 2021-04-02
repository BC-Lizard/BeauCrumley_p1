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
        private readonly IDataSaver _repoDataSaver;
        public UserMethods() { }
        public UserMethods(IFactory factory, IDataFetcher repoDataFetcher, IDataSaver repoDataSaver)
        {
            _factory = factory;
            _repoDataFetcher = repoDataFetcher;
            _repoDataSaver = repoDataSaver;
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

        /// <summary>
        /// Saves a new user to database. Returns true if the operation was successful. Returns false if use already exists.
        /// userData is a string of data that makes up a user object, deliniated by '-'
        /// Data order is as follows: FirstName-LastName-Username-PasswordSalt-HashedPassword-Phonenumber-Email
        /// Permission level and default store data need to be appended in that order
        /// </summary>
        /// <param name="userData"></param>
        /// <returns></returns>
        public bool registerNewUser(string userData)
        {
            string[] parsedData = userData.Split("-");
            List<string> objectData = new List<string>();
            
            objectData.Add("0");//add placeholder account no
            //copy parsed data into mutable list
            foreach (var i in parsedData)
            {
                objectData.Add(i);
            }
            objectData.Add("1");//add default permissions
            objectData.Add("3");//add default store

            IAUser newUser = _factory.CreateUser(objectData, _factory.CreateLogger());

            if (IsUser(newUser.Email))
            {
                //user exists, don't add to database.
                return false;
            }
            else
            {
                _repoDataSaver.RepoSaveNewUser(objectData);
                return true;
            }
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

        public IAStore GetStores(int Id)
        {
            var data = _repoDataFetcher.RepoGetStoreData(Id);
            IAStore newStore = _factory.CreateStore(data[0], _factory.CreateLogger());
            InitStoreState(newStore);
            InitStoreInventory(newStore);
            return newStore;
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

        public bool saveNewOrder(string orderData, string itemData)
        {
            string[] parsedData = orderData.Split("-");
            string[] parsedItems = itemData.Split("_");

            List<List<string>> parsedItemData = new List<List<string>>();
            for (var i = 0; i < parsedItems.Length; i++)
            {
                parsedItemData.Add(new List<string>());
                string[] parsedItem = parsedItems[i].Split("-");
                foreach (var elem in parsedItem)
                {
                    parsedItemData[i].Add(elem);
                }
            }

            List<string> objectData = new List<string>();
            
            objectData.Add("0");//add placeholder order no
            //copy parsed data into mutable list
            foreach (var i in parsedData)
            {
                objectData.Add(i);
            }

            IAOrder newOrder = _factory.CreateOrder(objectData, _factory.CreateLogger());

            if (_repoDataSaver.RepoSaveNewOrder(objectData, parsedItemData) == true) {
                return true;
            } else {
                return false;
            }
            
        }
    }
}