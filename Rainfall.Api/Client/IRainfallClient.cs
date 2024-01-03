using Rainfall.Api.Models;

namespace Rainfall.Api.Client
{
    public interface IRainfallClient
    {
        Task<RainfallReadingResponse> GetStationMeasures(string stationId, int count);
    }
}
