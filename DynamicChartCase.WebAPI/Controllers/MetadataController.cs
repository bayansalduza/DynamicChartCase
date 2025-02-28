using Microsoft.AspNetCore.Mvc;
using DynamicChartCase.Application.Services;
using DynamicChartCase.Domain.Entities;
using System;

namespace DynamicChartCase.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MetadataController : ControllerBase
    {
        private readonly IMetadataService _metadataService;

        public MetadataController(IMetadataService metadataService)
        {
            _metadataService = metadataService;
        }

        [HttpPost("GetDataSources")]
        public IActionResult GetDataSources([FromBody] ConnectionParameters connParams)
        {
            try
            {
                var list = _metadataService.GetDataSources(connParams);

                return Ok(list);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
