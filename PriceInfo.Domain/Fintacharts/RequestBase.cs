using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PriceInfo.Domain.Fintacharts
{
    // {"type":"l1-subscription","id":"1","instrumentId":"ad9e5345-4c3b-41fc-9437-1d253f62db52","provider":"simulation","subscribe":true,"kinds":["ask","bid","last"]}
    public class RequestBase : ResponseBase
    {
        [JsonPropertyName("instrumentId")]
        public string? InstrumentId { get; set; }

        [JsonPropertyName("provider")]
        public string? Provider { get; set; } = "simulation";
    }
}
