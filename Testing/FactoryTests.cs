using System;
using System.Collections.Generic;
using BusinessLogic.Models;
using BusinessLogic.Logging;
using Xunit;
using BusinessLogic;

namespace Testing
{
    public class FactoryTests
    {
        [Fact]
        public void CreateOrderTest()
        {
            IFactory factory = new Factory();
            List<string> mockData = new List<string>() {"7", "7", "7", "7", "7.77", "7.77", "7.77"};
            IAOrder expected = new AOrder(mockData, new ConsoleLogger());
            
            IAOrder actual = factory.CreateOrder(mockData, new ConsoleLogger());
            Assert.True(expected.OrderNo.Equals(actual.OrderNo));
        }

        [Fact]
        public void CreateItemTest()
        {
            IFactory factory = new Factory();
            List<string> mockData = new List<string>() {"7", "testpart", "I AM A TEST PART", "7.77", "7.77", "../"};
            IAItem expected = new AItem(mockData, new ConsoleLogger());
            
            IAItem actual = factory.CreateItem(mockData, new ConsoleLogger());
            Assert.True(expected.PartDescription.Equals(actual.PartDescription));
        }

        [Fact]
        public void CreateStateTest()
        {
            IFactory factory = new Factory();
            List<string> mockData = new List<string>() {"7", "Test State", "ts", "0.77"};
            IAState expected = new AState(mockData, new ConsoleLogger());
            
            IAState actual = factory.CreateState(mockData, new ConsoleLogger());
            Assert.True(expected.StateCode.Equals(actual.StateCode));
        }

        [Fact]
        public void CreateStoreTest()
        {
            IFactory factory = new Factory();
            List<string> mockData = new List<string>() {"7", "Test Store", "Test City", "7", "77777", "777 Test St."};
            IAStore expected = new AStore(mockData, new ConsoleLogger());
            
            IAStore actual = factory.CreateStore(mockData, new ConsoleLogger());
            Assert.True(expected.StoreStreetAddress.Equals(actual.StoreStreetAddress));
        }

        [Fact]
        public void CreateUserTest()
        {
            IFactory factory = new Factory();
            List<string> mockData = new List<string>() {"7", "TestFirst", "TestLast", "Testuser777", "testsalt", "testhash", "0000000000", "test@email.com", "7", "7"};
            IAUser expected = new AUser(mockData, new ConsoleLogger());
            
            IAUser actual = factory.CreateUser(mockData, new ConsoleLogger());
            Assert.True(expected.Username.Equals(actual.Username));
        }
    }
}
