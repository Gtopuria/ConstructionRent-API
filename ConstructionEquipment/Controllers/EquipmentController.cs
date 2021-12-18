using ConstructionEquipment.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ConstructionEquipment.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class EquipmentController : ControllerBase
	{
		private readonly ILogger<EquipmentController> _logger;
		private IInventoryService _inventoryService;

		public EquipmentController(ILogger<EquipmentController> logger, IInventoryService inventoryService)
		{
			_logger = logger;
			_inventoryService = inventoryService;
		}

		[HttpGet]
		[Produces("application/json")]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
		[ProducesResponseType(typeof(Equipment), (int)HttpStatusCode.OK)]
		public ActionResult<Equipment> Get() 
		{
            try
            {
				return Ok(_inventoryService.GetAll());
            }
            catch
            {
               return StatusCode(500);
			}
		}
	}
}
