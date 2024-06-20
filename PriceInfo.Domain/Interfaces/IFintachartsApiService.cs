using PriceInfo.Domain.Fintacharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceInfo.Domain.Interfaces
{
    public interface IFintachartsApiService
    {
        Task<string> GetAccessTokenAsync(CancellationToken cancellationToken);
        Task<InstrumentsResponse> GetInstrumentsAsync(CancellationToken cancellationToken);
    }
}
