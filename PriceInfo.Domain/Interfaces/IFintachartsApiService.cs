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
        /// <summary>
        /// Returns access token to access endpoins of fintacharts API
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<string> GetAccessTokenAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Returns instrument list
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<InstrumentsResponse> GetInstrumentsAsync(CancellationToken cancellationToken);
    }
}
