using Microsoft.EntityFrameworkCore;
using PriceInfo.Application.Helpers;
using PriceInfo.Domain.Entities;
using PriceInfo.Domain.Interfaces;
using PriceInfo.Infrastructure.Data;

namespace PriceInfo.Application.Services.Fintacharts
{
    public class FintachartsLogic : IFintachartsLogic
    {
        private readonly AppDbContext _dbContext;
        private readonly IFintachartsWebSocketService _fintachartsWebSocketService;

        public FintachartsLogic(AppDbContext dbContext,
            IFintachartsWebSocketService fintachartsWebSocketService
            )
        {
            _dbContext = dbContext;
            _fintachartsWebSocketService = fintachartsWebSocketService;
        }

        public async Task<IEnumerable<AssetPriceInfo>> GetPriceInfo(string assetsListStr)
        {
            var result = new List<AssetPriceInfo>();
            var instruments = await _dbContext.Assets.ToListAsync();
            var assetsList = assetsListStr.Split([',', ';']);
            foreach (var asset in assetsList)
            {
                var symbol = NameHelper.RemoveAllNonLetterChars(asset);
                var instrument = instruments.Find(x => x.Symbol.Equals(symbol, StringComparison.InvariantCultureIgnoreCase));
                if (instrument != null)
                {
                    var infos = await _fintachartsWebSocketService.GetUpdates(instrument.InstrumentId, 1);
                    if (infos.Count() > 0)
                    {
                        result.Add(new AssetPriceInfo
                        {
                            AssetName = instrument.Description,
                            Price = infos.Last().Price,
                            UpdateTime = infos.Last().UpdateTime.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss \"GMT\"zzz")
                        });
                    }
                }
            }
            return result;
        }
    }
}
