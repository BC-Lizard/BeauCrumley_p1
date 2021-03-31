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
    }
}