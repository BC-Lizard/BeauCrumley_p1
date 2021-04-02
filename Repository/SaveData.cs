using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Repository.dbModels;

namespace Repository
{
    public class DataSaver : IDataSaver
    {
        private Project1_DBContext _db;
        public DataSaver() { }
        public DataSaver(Project1_DBContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Returns a list of all user data from the database
        /// </summary>
        public void RepoSaveNewUser(List<string> data)
        {
            User newUser = new User();
            newUser.Firstname = data[1];
            newUser.Lastname = data[2];
            newUser.Username = data[3];
            newUser.PasswordSalt = data[4];
            newUser.PasswordHash = data[5];
            newUser.Phone = data[6];
            newUser.Email = data[7];
            newUser.Permission = int.Parse(data[8]);
            newUser.DefaultStore = int.Parse(data[9]);

            _db.Users.Add(newUser);
            _db.SaveChanges();
        }

        public bool RepoSaveNewOrder(List<string> data, List<List<string>> items)
        {
            Order newOrder = new Order();
            DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(long.Parse(data[3]));
            //newOrder.OrderNo = int.Parse(data[0]);
            newOrder.StoreNo = int.Parse(data[1]);
            newOrder.AccountNo = int.Parse(data[2]);
            newOrder.OrderDate = dateTimeOffset.DateTime;
            newOrder.Subtotal = decimal.Parse(data[4]);
            newOrder.Tax = decimal.Parse(data[5]);
            newOrder.Total = decimal.Parse(data[6]);
            
            _db.Orders.Add(newOrder);
            _db.SaveChanges();

            //get new order number
            var recentOrder = _db.Orders
                .Where(i => i.OrderDate == newOrder.OrderDate)
                .ToList();
                
            List<OrderHistory> junct = new List<OrderHistory>();
            for (int i = 0; i < items.Count; i++)
            {
                junct.Add(new OrderHistory());
                junct[i].OrderNo = recentOrder[0].OrderNo;
                junct[i].PartNo = int.Parse(items[i][0]);
                junct[i].UnitPrice = decimal.Parse(items[i][1]);
                junct[i].Quantity = int.Parse(items[i][2]);
                _db.Inventories.SingleOrDefault(item => item.StoreNo == newOrder.StoreNo && item.PartNo == junct[i].PartNo).Quantity -= junct[i].Quantity;
            }
            foreach (var item in junct)
            {
                _db.OrderHistories.Add(item);
            }
            _db.SaveChanges();
            return true;
        }
    }
}