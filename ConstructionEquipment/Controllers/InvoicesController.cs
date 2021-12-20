using ConstructionEquipment.Domain.Invoice;
using Microsoft.AspNetCore.Http;
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
    public class InvoicesController : ControllerBase
    {
        public IInvoiceService _invoiceService;
		private readonly ILogger<InvoicesController> _logger;

        public InvoicesController(IInvoiceService invoiceService, ILogger<InvoicesController> logger)
        {
            _invoiceService = invoiceService;
            _logger = logger;

        }

        [HttpGet("{orderId}")]
        public ActionResult<Invoice> Get(Guid orderId)
        {
            try
            {
                return Ok(_invoiceService.Generate(orderId));
            }
            catch (ArgumentException e)
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
