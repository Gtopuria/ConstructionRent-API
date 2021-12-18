using ConstructionEquipment.Domain.Order;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConstructionEquipment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {

        private readonly ILogger<OrdersController> _logger;
        private IRentOrderService _rentOrderService;
        public OrdersController(ILogger<OrdersController> logger, IRentOrderService rentOrderService)
        {
            _rentOrderService = rentOrderService;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<List<RentOrder>> Get()
        {
            return Ok(_rentOrderService.GetAll());
        }

        // GET api/<OrderController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<OrderController>
        [HttpPost]
        public void Post([FromBody] RentOrder order)
        {
            _rentOrderService.PlaceOrder(order);
        }

        // PUT api/<OrderController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<OrderController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            try
            {
                _rentOrderService.Remove(id);
                return Ok("Order was removed");
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e.Message);
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
