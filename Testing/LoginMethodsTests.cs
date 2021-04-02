using System;
using System.Collections.Generic;
using BusinessLogic;
using BusinessLogic.Models;
using Repository;
using Repository.dbModels;
using Xunit;

namespace Testing
{
    public class LoginMethodsTests
    {
        public ILoginMethods loginMethods { get; set; } = new LoginMethods(new Factory(), new DataFetcher(new Project1_DBContext()), new UserMethods(new Factory(), new DataFetcher(new Project1_DBContext()), new DataSaver(new Project1_DBContext())));
        public IUserMethods userMethods { get; set; } = new UserMethods(new Factory(), new DataFetcher(new Project1_DBContext()), new DataSaver(new Project1_DBContext()));

        [Fact]
        public void TestCheckCreds()
        {
            IAUser mockUser = userMethods.BlankUser();
            bool result = loginMethods.CheckCreds(mockUser, "_");
            var expected = true;

            var actual = result;

            Assert.True(expected.Equals(actual));
        }
        
        [Fact]
        public void TestCheckCredsFail()
        {
            IAUser mockUser = userMethods.BlankUser();
            bool result = loginMethods.CheckCreds(mockUser, "Pizza");
            var expected = false;

            var actual = result;

            Assert.True(expected.Equals(actual));
        }
    }
}