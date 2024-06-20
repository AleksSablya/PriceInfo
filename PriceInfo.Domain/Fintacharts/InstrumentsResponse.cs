using System.Text.Json.Serialization;

namespace PriceInfo.Domain.Fintacharts
{
    public class InstrumentsResponse
    {
        [JsonPropertyName("paging")]
        public Paging Paging { get; set; }

        [JsonPropertyName("data")]
        public Instrument[] Data { get; set; }
    }

    public class Paging
    {
        public int Page { get; set; }
        public int Pages { get; set; }
        public int Items { get; set; }
    }

    public class Instrument
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }

        [JsonPropertyName("kind")]
        public string Kind { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("tickSize")]
        public double TickSize { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        [JsonPropertyName("baseCurrency")]
        public string BaseCurrency { get; set; }

        [JsonPropertyName("mappings")]
        public ProviderMappings Mappings { get; set; }
    }

    public class ProviderMappings
    {
        [JsonPropertyName("active-tick")]
        public ProviderInfo? ActiveTick { get; set; }

        [JsonPropertyName("alpaca")]
        public ProviderInfo? Alpaca { get; set; }

        [JsonPropertyName("cryptoquote")]
        public ProviderInfo? Cryptoquote { get; set; }

        [JsonPropertyName("dxfeed")]
        public ProviderInfo? Dxfeed { get; set; }

        [JsonPropertyName("oanda")]
        public ProviderInfo? Oanda { get; set; }

        [JsonPropertyName("simulation")]
        public ProviderInfo? Simulation { get; set; }
    }


    public class ProviderInfo
    {
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }

        public string exchange { get; set; }

        public int defaultOrderSize { get; set; }
    }
}
