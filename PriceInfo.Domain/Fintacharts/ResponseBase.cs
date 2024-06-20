using System.Text.Json.Serialization;

namespace PriceInfo.Domain.Fintacharts
{
    public class ResponseBase
    {
        [JsonPropertyName("type")]
        public string? Type { get; set; }
    }
}
