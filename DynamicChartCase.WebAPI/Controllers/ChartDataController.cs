using DynamicChartCase.Application.Services;
using DynamicChartCase.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DynamicChartCase.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChartDataController : ControllerBase
    {
        private readonly IChartDataService _chartDataService;

        public ChartDataController(IChartDataService chartDataService)
        {
            _chartDataService = chartDataService;
        }

        [HttpPost("GetData")]
        public IActionResult GetData([FromBody] ChartDataRequest request)
        {
            try
            {
                var data = _chartDataService.GetData(request.ConnectionParameters, request.DataSourceName, request.ObjectType, request.Parameters
                );

                return Ok(new { records = data });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
