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
    public class StoresController : ControllerBase
    {
        private readonly IUserMethods _userMethods;
        public StoresController(IUserMethods userMethods)
        {
            _userMethods = userMethods;
        }

        // GET: api/<StoresController>
        [HttpGet]
        public List<IAStore> Get()
        {
            return _userMethods.GetStores();
        }

        // GET api/<StoresController>/5
        [HttpGet("{id}")]
        public IAStore Get(int id)
        {
            return _userMethods.GetStores(id);
        }
    }
}
