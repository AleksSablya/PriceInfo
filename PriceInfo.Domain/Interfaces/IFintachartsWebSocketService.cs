using PriceInfo.Domain.Entities;

namespace PriceInfo.Domain.Interfaces
{
    public interface IFintachartsWebSocketService
    {
        Task<IEnumerable<UpdateInfo>> GetUpdates(string instrumentId, int fetchCount = 1);
    }
}