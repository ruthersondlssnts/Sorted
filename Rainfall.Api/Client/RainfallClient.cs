using Newtonsoft.Json;
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
            var readings = await _httpClient.GetAsync($"flood-monitoring/id/stations/{stationId}/readings?_sorted&_limit={count}");

            var jsonResponse = await readings.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<RainfallReadingResponse>(jsonResponse);
        }
    }
}
