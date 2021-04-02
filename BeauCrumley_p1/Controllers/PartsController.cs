using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.Models;
using BusinessLogic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BeauCrumley_p1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartsController : ControllerBase
    {
        private readonly IUserMethods _userMethods;
        public PartsController(IUserMethods userMethods)
        {
            _userMethods = userMethods;
        }

        // GET: api/<PartsController>
        [HttpGet]
        public List<IAItem> Get()
        {
            return _userMethods.GetAllItems();
        }
    }
}
