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


        // POST api/<OrderController>
        [HttpPost]
        public ActionResult Post([FromBody] RentOrder order)
        {
            try
            {
                _rentOrderService.PlaceOrder(order);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500);
            }
            
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
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500);
            }
        }
    }
}
