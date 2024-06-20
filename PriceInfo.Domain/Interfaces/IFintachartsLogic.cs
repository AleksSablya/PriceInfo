using PriceInfo.Domain.Entities;

namespace PriceInfo.Domain.Interfaces
{ 
    public interface IFintachartsLogic
    {
        Task<IEnumerable<AssetPriceInfo>> GetPriceInfo(string assetsListStr);
    }
}