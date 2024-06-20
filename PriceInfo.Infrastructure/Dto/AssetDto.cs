using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceInfo.Domain.Entities
{
    public class AssetDto
    {
        public int Id { get; set; }
        public string Symbol { get; set; }
        public string Description { get; set; }
        public string InstrumentId { get; set; }
    }
}
