using PriceInfo.Domain.Entities;

namespace PriceInfo.Domain.Interfaces
{ 
    public interface IFintachartsLogic
    {
        /// <summary>
        /// Returns assets prices defined in assetsListStr like comma separated list of asses. E.g. EURUSD,EURGBP,GBPAUD
        /// </summary>
        /// <param name="assetsListStr"></param>
        /// <returns></returns>
        Task<IEnumerable<AssetPriceInfo>> GetPriceInfo(string assetsListStr);
    }
}