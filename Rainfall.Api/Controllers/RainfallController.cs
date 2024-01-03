using Microsoft.AspNetCore.Mvc;
using Rainfall.Api.Client;

namespace Rainfall.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RainfallController : ControllerBase
    {
        readonly IRainfallClient _stationMeasuresClient;

        public RainfallController(IRainfallClient stationMeasuresClient)
        {
            _stationMeasuresClient = stationMeasuresClient;
        }

        [HttpGet]
        [Route("id/{stationId}/readings")]
        public async Task<IActionResult> Get(string stationId, int count = 100)
        {
            var measures = await _stationMeasuresClient.GetStationMeasures(stationId, count);

            return measures == null ? NotFound() : Ok(measures);
        }
    }
}