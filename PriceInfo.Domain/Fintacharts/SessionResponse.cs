using System.Text.Json.Serialization;

namespace PriceInfo.Domain.Fintacharts
{
    // {"type":"session","sessionId":"fe0751f6-2951-439d-9281-566fbee156bb"}
    // {"type":"response","requestId":"1"}
    public class SessionResponse : ResponseBase
    {
        [JsonPropertyName("sessionId")]
        public string? SessionId { get; set; }

        [JsonPropertyName("requestId")]
        public string? RequestId { get; set; }

        [JsonPropertyName("error")]
        public Error? Error { get; set; }
        
    }

    // {"code":"instrument_not_found","message":"Instrument \u0027cf14568a-5f99-47bd-aec0-7e224bfd5cf4\u0027 not found."}
    public class Error
    {
        [JsonPropertyName("code")]
        public string? Code { get; set; }

        [JsonPropertyName("message")]
        public string? Message { get; set; }
    }
}
