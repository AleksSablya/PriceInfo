using System.Text.Json.Serialization;

namespace PriceInfo.Domain.Fintacharts
{
    public class Request : RequestBase
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("kinds")]
        public string[] Kinds { get; set; } = new[] { "ask", "bid", "last" };

        [JsonPropertyName("subscribe")]
        public bool Subscribe { get; set; } = true;
    }
}
