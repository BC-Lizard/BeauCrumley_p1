using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Repository.dbModels;

namespace Repository
{
    public class DataFetcher : IDataFetcher
    {
        private Project1_DBContext _db;
        public DataFetcher() { }
        public DataFetcher(Project1_DBContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Returns a list of all user data from the database
        /// </summary>
        public List<List<string>> RepoGetUserData()
        {
            var results = new List<List<string>>();
            var userTableData = _db.Users.FromSqlRaw("SELECT * FROM Users;").ToList();
            foreach (var user in userTableData)
            {
                results.Add(new List<string>()
                {
                    user.AccountNo.ToString(),
                    user.Firstname,
                    user.Lastname,
                    user.Username,
                    user.PasswordSalt,
                    user.PasswordHash,
                    user.Phone,
                    user.Email,
                    user.Permission.ToString(),
                    user.DefaultStore.ToString()
                });
            }
            return results;
        }
        public List<List<string>> RepoGetUserData(string email)
        {
            var results = new List<List<string>>();
            var userTableData = _db.Users
                .Where(i => i.Email == email)
                .ToList();
            foreach (var user in userTableData)
            {
                results.Add(new List<string>()
                {
                    user.AccountNo.ToString(),
                    user.Firstname,
                    user.Lastname,
                    user.Username,
                    user.PasswordSalt,
                    user.PasswordHash,
                    user.Phone,
                    user.Email,
                    user.Permission.ToString(),
                    user.DefaultStore.ToString()
                });
            }
            return results;
        }
        public bool RepoIsUser(string email)
        {
            var userTableData = _db.Users
                .Where(i => i.Email == email)
                .ToList();
            if (userTableData.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Returns a list of all store data from the database
        /// </summary>
        public List<List<string>> RepoGetStoreData()
        {
            var results = new List<List<string>>();
            var storeTableData = _db.Stores.FromSqlRaw("SELECT * FROM Stores;").ToList();
            foreach (var store in storeTableData)
            {
                results.Add(new List<string>()
                {
                    store.StoreNo.ToString(),
                    store.StoreName,
                    store.StoreCity,
                    store.StoreState.ToString(),
                    store.StoreZipCode.ToString(),
                    store.StoreStreetAddress
                });
            }
            return results;
        }

        /// <summary>
        /// Overload of RepoGetStoreData that returns store data from the database that matches an Id
        /// </summary>
        /// <param name="Id"></param>
        public List<List<string>> RepoGetStoreData(int Id)
        {
            var results = new List<List<string>>();
            var storeTableData = _db.Stores.FromSqlRaw($"SELECT * FROM Stores WHERE StoreNo = {Id};").ToList();
            foreach (var store in storeTableData)
            {
                results.Add(new List<string>()
                {
                    store.StoreNo.ToString(),
                    store.StoreName,
                    store.StoreCity,
                    store.StoreState.ToString(),
                    store.StoreZipCode.ToString(),
                    store.StoreStreetAddress
                });
            }
            return results;
        }

        public List<List<string>> RepoGetStateData(int Id)
        {
            var results = new List<List<string>>();
            var storeTableData = _db.States.FromSqlRaw($"SELECT * FROM States WHERE StateNo = {Id};").ToList();
            foreach (var store in storeTableData)
            {
                results.Add(new List<string>()
                {
                    store.StateNo.ToString(),
                    store.StateName,
                    store.StateCode,
                    store.TaxRate.ToString()
                });
            }
            return results;
        }

        public List<List<string>> RepoGetItemData()
        {
            var results = new List<List<string>>();
            var itemTableData = _db.Items.FromSqlRaw("SELECT * FROM Items;").ToList();
            foreach (var part in itemTableData)
            {
                results.Add(new List<string>()
                {
                    part.PartNo.ToString(),
                    part.PartName,
                    part.PartDescription,
                    part.PartPrice.ToString(),
                    part.PartSale.ToString(),
                    part.PartImage
                });
            }
            return results;
        }
        public List<List<string>> RepoGetItemData(int Id)
        {
            var results = new List<List<string>>();
            var itemTableData = _db.Items.FromSqlRaw($"SELECT * FROM Items WHERE PartNo = {Id}").ToList();
            foreach (var part in itemTableData)
            {
                results.Add(new List<string>()
                {
                    part.PartNo.ToString(),
                    part.PartName,
                    part.PartDescription,
                    part.PartPrice.ToString(),
                    part.PartSale.ToString(),
                    part.PartImage
                });
            }
            return results;
        }
        public List<List<int>> RepoGetInvDataForStore(int Id)
        {
            var results = new List<List<int>>();
            var invTableData = _db.Inventories
                .Where(i => i.StoreNo == Id)
                .ToList();
            foreach (var part in invTableData)
            {
                results.Add(new List<int>()
                {
                    part.PartNo,
                    part.Quantity
                });
            }
            return results;
        }
    }
}