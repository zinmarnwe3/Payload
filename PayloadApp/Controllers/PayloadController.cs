using Microsoft.AspNetCore.Mvc;
using PayloadApp.Contracts;
using PayloadApp.ViewModels;

namespace PayloadApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayloadController : ControllerBase
    {
        private readonly ILogger<PayloadController> _logger;
        private readonly IPayloadService _payloadService;

        public PayloadController(ILogger<PayloadController> logger, IPayloadService payloadService)
        {
            _logger = logger;
            _payloadService = payloadService;
        }

        [HttpPost]
        public async Task<IActionResult> ReceivePayload(PayloadDto payloadDto)
        {
            var result = await _payloadService.StorePayload(payloadDto);

            if (result)
            {
                return Ok();
            }
            else
            {
                return StatusCode(500, "An error occurred while storing the payload.");
            }
        }
    }
}