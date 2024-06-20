using System.Text.Json.Serialization;

namespace PriceInfo.Domain.Fintacharts
{
    //{
    //"type": "l1-update",
    //"instrumentId": "ad9e5345-4c3b-41fc-9437-1d253f62db52",
    //"provider": "simulation",
    //"last": {
    //	"timestamp": "2024-06-18T06:19:52.9806595+00:00",
    //	"price": 171.2,
    //	"volume": 257,
    //	"change": -0.77,
    //	"changePct": -0.45
    //}

    public class UpdateResponse : RequestBase
    {
        [JsonPropertyName("ask")]
        public Kind? Ask { get; set; }

        [JsonPropertyName("bid")]
        public Kind? Bid { get; set; }

        [JsonPropertyName("last")]
        public Kind? Last { get; set; }
    }

    public class Kind
    {
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }

        [JsonPropertyName("price")]
        public double Price { get; set; }

        [JsonPropertyName("volume")]
        public double Volume { get; set; }

        [JsonPropertyName("change")]
        public double? Change { get; set; }

        [JsonPropertyName("changePct")]
        public double? ChangePct { get; set; }
    }
}
