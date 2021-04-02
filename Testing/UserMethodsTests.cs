using System;
using System.Collections.Generic;
using BusinessLogic;
using BusinessLogic.Models;
using Repository;
using Repository.dbModels;
using Xunit;

namespace Testing
{
    public class UserMethodsTests
    {
        public IUserMethods userMethods { get; set; } = new UserMethods(new Factory(), new DataFetcher(new Project1_DBContext()), new DataSaver(new Project1_DBContext()));
        
        [Fact]
        public void TestGetUsers()
        {
            List<IAUser> users = userMethods.GetUsers();
            var expected = true;

            var actual = users.Count > 0;

            Assert.True(expected.Equals(actual));
        }

        [Fact]
        public void TestGetUsersOverload()
        {
            List<IAUser> user = userMethods.GetUsers("test@email.com");
            var expected = 1;

            var actual = user[0].AccountNo;

            Assert.True(expected.Equals(actual));
        }

        [Fact]
        public void TestGetUsersOverloadUsername()
        {
            List<IAUser> user = userMethods.GetUsers("test@email.com");
            var expected = "TestUser";

            var actual = user[0].Username;

            Assert.True(expected.Equals(actual));
        }

        [Fact]
        public void TestGetUsersOverloadFirstname()
        {
            List<IAUser> user = userMethods.GetUsers("test@email.com");
            var expected = "testfirst";

            var actual = user[0].Firstname;

            Assert.True(expected.Equals(actual));
        }

        [Fact]
        public void TestGetUsersOverloadlastname()
        {
            List<IAUser> user = userMethods.GetUsers("test@email.com");
            var expected = "testlast";

            var actual = user[0].Lastname;

            Assert.True(expected.Equals(actual));
        }

        [Fact]
        public void TestGetUsersOverloadSalt()
        {
            List<IAUser> user = userMethods.GetUsers("test@email.com");
            var expected = "testsalt";

            var actual = user[0].PasswordSalt;

            Assert.True(expected.Equals(actual));
        }

        [Fact]
        public void TestGetUsersOverloadHash()
        {
            List<IAUser> user = userMethods.GetUsers("test@email.com");
            var expected = "382d700319077f8a057f95a94d67be197842e3a7a1cd522488e1763cb7122051";

            var actual = user[0].PasswordHash;

            Assert.True(expected.Equals(actual));
        }

        [Fact]
        public void TestGetUsersOverloadPhone()
        {
            List<IAUser> user = userMethods.GetUsers("test@email.com");
            var expected = "0123456789";

            var actual = user[0].Phone;

            Assert.True(expected.Equals(actual));
        }

        [Fact]
        public void TestGetUsersOverloadEmail()
        {
            List<IAUser> user = userMethods.GetUsers("test@email.com");
            var expected = "test@email.com";

            var actual = user[0].Email;

            Assert.True(expected.Equals(actual));
        }

        [Fact]
        public void TestGetUsersOverloadPermission()
        {
            List<IAUser> user = userMethods.GetUsers("test@email.com");
            var expected = 7;

            var actual = user[0].Permission;

            Assert.True(expected.Equals(actual));
        }

        [Fact]
        public void TestGetUsersOverloadStore()
        {
            List<IAUser> user = userMethods.GetUsers("test@email.com");
            var expected = 1;

            var actual = user[0].DefaultStore;

            Assert.True(expected.Equals(actual));
        }
        
        [Fact]
        public void TestIsUser()
        {
            bool result = userMethods.IsUser("test@email.com");
            var expected = true;

            var actual = result;

            Assert.True(expected.Equals(actual));
        }
        
        [Fact]
        public void TestIsUserFail()
        {
            bool result = userMethods.IsUser("none");
            var expected = false;

            var actual = result;

            Assert.True(expected.Equals(actual));
        }
        
        [Fact]
        public void TestregisterNewUser()
        {
            string mockData = "test-test-test-test-test-0000000000-test@email.com";
            bool result = userMethods.registerNewUser(mockData);
            var expected = false;

            var actual = result;

            Assert.True(expected.Equals(actual));
        }
        
        [Fact]
        public void TestBlankUser()
        {
            IAUser user = userMethods.BlankUser();
            var expected = "_";

            var actual = user.Firstname;

            Assert.True(expected.Equals(actual));
        }
        
        [Fact]
        public void TestGetStores()
        {
            List<IAStore> stores = userMethods.GetStores();
            var expected = true;

            var actual = stores.Count > 0;

            Assert.True(expected.Equals(actual));
        }
        
        [Fact]
        public void TestGetStoresOverload()
        {
            IAStore stores = userMethods.GetStores(1);
            var expected = 1;

            var actual = stores.StoreNo;

            Assert.True(expected.Equals(actual));
        }
        
        [Fact]
        public void TestGetState()
        {
            IAState state = userMethods.GetState(1);
            var expected = 1;

            var actual = state.StateNo;

            Assert.True(expected.Equals(actual));
        }
        
        [Fact]
        public void TestGetItem()
        {
            IAItem item = userMethods.GetItem(1);
            var expected = 1;

            var actual = item.PartNo;

            Assert.True(expected.Equals(actual));
        }
        
        [Fact]
        public void TestGetAllItems()
        {
            List<IAItem> items = userMethods.GetAllItems();
            var expected = true;

            var actual = items.Count > 0;

            Assert.True(expected.Equals(actual));
        }
    }
}