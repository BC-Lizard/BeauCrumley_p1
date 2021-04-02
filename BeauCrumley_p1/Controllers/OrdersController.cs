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
    public class OrdersController : ControllerBase
    {
        private readonly IUserMethods _userMethods;
        public OrdersController(IUserMethods userMethods)
        {
            _userMethods = userMethods;
        }

        // GET: api/<OrdersController>
        [HttpGet]
        public bool Get()
        {
            return true;
        }

        // POST api/<OrdersController>
        [HttpPost("{orderData}/{itemData}")]
        public string Post(string orderData, string itemData)
        {
            if (_userMethods.saveNewOrder(orderData, itemData) == true)
            {
                return "Order Successful";
            }
            else
            {
                return "Order Failed To Complete.";
            }
        }

        // PUT api/<OrdersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<OrdersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
