using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PriceInfo.Application.Helpers;
using PriceInfo.Domain.Entities;
using PriceInfo.Domain.Interfaces;
using PriceInfo.Infrastructure.Data;
using System.Text.RegularExpressions;

namespace PriceInfo.Application.Services.Fintacharts
{
    /// <summary>
    /// Reads Instruments from API and saves them to Database
    /// </summary>
    public class FintachartsWorker : BackgroundService
    {
        private readonly IFintachartsApiService _fintachartsService;
        private readonly IServiceScopeFactory serviceScopeFactory;

        public FintachartsWorker(
            IFintachartsApiService fintachartsService,
            IServiceScopeFactory serviceScopeFactory)
        {
            _fintachartsService = fintachartsService;
            this.serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using (var scope = serviceScopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<AppDbContext>();
                var assets = await dbContext!.Assets.ToListAsync();
                var instruments = await _fintachartsService.GetInstrumentsAsync(stoppingToken);
                if (instruments?.Data != null)
                {
                    var rgx = new Regex("[^a-zA-Z]");
                    foreach (var instr in instruments.Data)
                    {
                        var asset = assets.Find(x => x.InstrumentId == instr.Id);
                        if (asset == null)
                        {
                            var newAsset = new AssetDto
                            {
                                InstrumentId = instr.Id,
                                Description = instr.Description,
                                Symbol = NameHelper.RemoveAllNonLetterChars(instr.Symbol),

                            };
                            dbContext.Assets.Add(newAsset);
                        }
                    }
                    await dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
