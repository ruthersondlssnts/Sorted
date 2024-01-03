using Newtonsoft.Json;

namespace Rainfall.Api.Models
{
    public class RainfallReading
    {
        [JsonProperty("dateTime")]
        public DateTime DateTime { get; set; }
        [JsonProperty("value")]
        public decimal AmountMeasured { get; set; }
    }
}
