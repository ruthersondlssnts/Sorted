using Rainfall.Api.Models;

namespace Rainfall.Api.Client
{
    public class RainfallClient : IRainfallClient
    {
        private readonly HttpClient _httpClient;

        public RainfallClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<RainfallReadingResponse> GetStationMeasures(string stationId, int count)
        {
            return await _httpClient.GetFromJsonAsync<RainfallReadingResponse>($"id/stations/{stationId}/readings?_sorted&_limit={count}");
        }
    }
}
