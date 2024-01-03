using Microsoft.AspNetCore.Mvc;
using Rainfall.Api.Client;
using Rainfall.Api.Models;

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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RainfallReadingResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> Get(string stationId, int count = 10)
        {
            //Validate
            if (count > 100 || count < 1 || string.IsNullOrEmpty(stationId))
            {
                var errorDetails = new List<ErrorDetail>();
                if (string.IsNullOrEmpty(stationId))
                    errorDetails.Add(new ErrorDetail { PropertyName = "stationId", Message = "StationId is Required" });

                if (count > 100 || count < 1)
                    errorDetails.Add(new ErrorDetail { PropertyName = "count", Message = "Must be minimum of 1 and maximum of 100" });

                return BadRequest(new ErrorResponse
                {
                    Message = "Invalid request",
                    Details = errorDetails
                });
            }

            //Get Measures
            try
            {
                var readings = await _stationMeasuresClient.GetStationMeasures(stationId, count);

                if (readings != null)
                {
                    if (readings.RainfallReadings.Count > 0)
                        return Ok(readings);
                    else
                        return NotFound(new ErrorResponse
                        {
                            Message = "No readings found for the specified stationId"
                        });
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse
                    {
                        Message = "Internal Server Error"
                    });
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse
                {
                    Message = "Internal Server Error"
                });
            }
        }
    }
}