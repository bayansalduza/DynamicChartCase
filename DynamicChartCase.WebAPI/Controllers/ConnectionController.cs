using Microsoft.AspNetCore.Mvc;
using DynamicChartCase.Application.Services;
using DynamicChartCase.Domain.Entities;

namespace DynamicChartCase.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConnectionController : ControllerBase
    {
        private readonly IConnectionService _connectionService;

        public ConnectionController(IConnectionService connectionService)
        {
            _connectionService = connectionService;
        }

        [HttpPost("TestConnection")]
        public IActionResult TestConnection([FromBody] ConnectionParameters connParams)
        {
            bool result = _connectionService.TestConnection(connParams);

            if (result)
                return Ok(new { message = "Bağlantı başarılı" });
            else
                return BadRequest(new { message = "Bağlantı başarısız" });
        }
    }
}
