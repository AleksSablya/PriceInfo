using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceInfo.Domain.Entities
{
    public class UpdateInfo
    {
        public string InstrumentId { get; set; }
        public DateTime UpdateTime { get; set; }
        public double Price { get; set; }
    }
}
