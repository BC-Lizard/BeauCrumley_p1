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

        // GET api/<PartsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<PartsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<PartsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PartsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
