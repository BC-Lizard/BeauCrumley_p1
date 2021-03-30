using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.Models;
using BusinessLogic;

namespace BeauCrumley_p1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IUserMethods _userMethods;
        private readonly ILoginMethods _loginMethods;
        public UsersController(IUserMethods userMethods, ILoginMethods loginMethods)
        {
            _userMethods = userMethods;
            _loginMethods = loginMethods;
        }

        [HttpGet]
        public List<IAUser> Get()
        {
            return _userMethods.GetUsers();
        }

        [HttpGet("{email}")]
        public string Get(string email)
        {
            if (_userMethods.IsUser(email)) {
                return _userMethods.GetUsers(email)[0].PasswordSalt;
            }
            else
            {
                return "_";
            }
        }

        [HttpPost("{hash}/{email}")]
        public IAUser Post(string hash, string email)
        {
            var user = _userMethods.GetUsers(email)[0];
            if (_loginMethods.CheckCreds(user, hash)) {
                return user;
            }
            else
            {
                return _userMethods.BlankUser();
            }
        }

        [HttpPost("{newUserData}")]
        public string Post(string newUserData)
        {
            Console.WriteLine("NEW USER DATA RECIEVED: " + newUserData);
            bool isNewUser = _userMethods.registerNewUser(newUserData);
            if (isNewUser)
            {
                return "1. Account Creation Successful";
            }
            else
            {
                return "0. User already exists.";
            }
        }
    }
}
