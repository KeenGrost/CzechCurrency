using Common.Data.Entity;
using Microsoft.EntityFrameworkCore;
using CzechCurrency.Entities;
namespace CzechCurrency.Data
{
    public class CzechCurrencyDbContext:DbContext
    {
        public CzechCurrencyDbContext(DbContextOptions<CzechCurrencyDbContext> options) : base(options)
        {
        }

        public DbSet<Currency> Currencies { get; set; }
        public DbSet<ExchangeRate> ExchangeRates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //todo индексы
            base.OnModelCreating(modelBuilder);
        }
        
    }
}
