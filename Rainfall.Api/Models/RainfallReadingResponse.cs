using Newtonsoft.Json;

namespace Rainfall.Api.Models
{
    public class RainfallReadingResponse
    {
        [JsonProperty("items")]
        public List<RainfallReading>? RainfallReadings { get; set; }
    }
}
