using Microsoft.EntityFrameworkCore;
using PriceInfo.Domain.Entities;

namespace PriceInfo.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<AssetDto> Assets { get; set; } = default!;
    }
}
