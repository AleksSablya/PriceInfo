using PriceInfo.Domain.Entities;

namespace PriceInfo.Domain.Interfaces
{
    public interface IFintachartsWebSocketService
    {
        /// <summary>
        /// Calls ClientWebSocket еo get data updates for a given instrument Id and kinf "last". Takes fetchCount updates.
        /// </summary>
        /// <param name="instrumentId"></param>
        /// <param name="fetchCount"></param>
        /// <returns></returns>
        Task<IEnumerable<UpdateInfo>> GetUpdates(string instrumentId, int fetchCount = 1);
    }
}