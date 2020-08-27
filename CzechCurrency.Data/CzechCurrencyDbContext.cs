using CzechCurrency.Entities;
using Microsoft.EntityFrameworkCore;

namespace CzechCurrency.Data
{
    public class CzechCurrencyDbContext : DbContext
    {
        public CzechCurrencyDbContext(DbContextOptions<CzechCurrencyDbContext> options) : base(options)
        {
        }

        public DbSet<Currency> Currencies { get; set; }
        public DbSet<ExchangeRate> ExchangeRates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExchangeRate>().HasIndex(a => a.CurrencyNumber);

            // Заполнение справочника валют

            var currencyItem1 = new Currency()
            {
                Number = "203",
                Code = "CZK",
                Name = "Чешская крона",
                Amount = 1
            };
            var currencyItem2 = new Currency()
            {
                Number = "643",
                Code = "RUB",
                Name = "Российский рубль",
                Amount = 1
            };
            var currencyItem3 = new Currency()
            {
                Number = "840",
                Code = "USD",
                Name = "Доллар США",
                Amount = 1
            };
            var currencyItem4 = new Currency()
            {
                Number = "978",
                Code = "EUR",
                Name = "Евро",
                Amount = 1
            };
            modelBuilder.Entity<Currency>().HasData(new[] { currencyItem1,currencyItem2,currencyItem3,currencyItem4
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
