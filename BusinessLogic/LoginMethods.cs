using BusinessLogic.Models;
using BusinessLogic.Logging;
using System;
using System.Collections.Generic;
using Repository;

namespace BusinessLogic
{
    public class LoginMethods : ILoginMethods
    {
        private readonly IFactory _factory;
        private readonly IDataFetcher _repoDataFetcher;
        private readonly IUserMethods _userMethods;
        public LoginMethods() { }
        public LoginMethods(IFactory factory, IDataFetcher repoDataFetcher, IUserMethods userMethods)
        {
            _factory = factory;
            _repoDataFetcher = repoDataFetcher;
            _userMethods = userMethods;
        }

        public bool CheckCreds(IAUser user, string hashedPassword)
        {
            if (user.PasswordHash == hashedPassword)
            {
                Console.WriteLine("Login Successful!");
                return true;
            }
            else
            {
                Console.WriteLine("Login Failed!");
                return false;
            }
        }
    }
}