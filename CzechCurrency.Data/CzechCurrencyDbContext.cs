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
            modelBuilder.Entity<ExchangeRate>().HasIndex(a => a.CurrencyCode);

            #region Заполнение справочника валют

            var currencyItem1 = new Currency() { Number = "036", Code = "AUD", Name = "Австралийский доллар", Amount = 1 };
            var currencyItem2 = new Currency() { Number = "975", Code = "BGN", Name = "Болгарский лев", Amount = 1};
            var currencyItem3 = new Currency() { Number = "840", Code = "BRL", Name = "Бразильский реал", Amount = 1};
            var currencyItem4 = new Currency() { Number = "756", Code = "CHF", Name = "Швейцарский франк", Amount = 1};
            var currencyItem5 = new Currency() { Number = "156", Code = "CNY", Name = "Юань", Amount = 1};

            var currencyItem6 = new Currency() { Number = "208", Code = "DKK", Name = "Датская крона", Amount = 1};
            var currencyItem7 = new Currency() { Number = "978", Code = "EUR", Name = "Евро", Amount = 1};
            var currencyItem8 = new Currency() { Number = "826", Code = "GBP", Name = "Фунт стерлингов", Amount = 1};
            var currencyItem9 = new Currency() { Number = "344", Code = "HKD", Name = "Гонконгский доллар", Amount = 1};
            var currencyItem10 = new Currency() { Number = "191", Code = "HRK", Name = "Хорватская куна", Amount = 1};

            var currencyItem11 = new Currency() { Number = "348", Code = "HUF", Name = "Форинт", Amount = 100};
            var currencyItem12 = new Currency() { Number = "360", Code = "IDR", Name = "Рупия", Amount = 1000};
            var currencyItem13 = new Currency() { Number = "376", Code = "ILS", Name = "Новый израильский шекель", Amount = 1};
            var currencyItem14 = new Currency() { Number = "352", Code = "ISK", Name = "Исландская крона", Amount = 100};
            var currencyItem15 = new Currency() { Number = "392", Code = "JPY", Name = "Иена", Amount = 100};

            var currencyItem16 = new Currency() { Number = "410", Code = "KRW", Name = "Вона", Amount = 100};
            var currencyItem17 = new Currency() { Number = "979", Code = "MXN", Name = "Мексиканское песо", Amount = 1 };
            var currencyItem18 = new Currency() { Number = "458", Code = "MYR", Name = "Малайзийский ринггит", Amount = 1 };
            var currencyItem19 = new Currency() { Number = "578", Code = "NOK", Name = "Норвежская крона", Amount = 1 };
            var currencyItem20 = new Currency() { Number = "554", Code = "NZD", Name = "Новозеландский доллар", Amount = 1 };

            var currencyItem21 = new Currency() { Number = "608", Code = "PHP", Name = "Филиппинское песо", Amount = 100 };
            var currencyItem22 = new Currency() { Number = "985", Code = "PLN", Name = "Злотый", Amount = 1};
            var currencyItem23 = new Currency() { Number = "946", Code = "RON", Name = "Новый румынский лей", Amount = 1 };
            var currencyItem24 = new Currency() { Number = "643", Code = "RUB", Name = "Российский рубль", Amount = 100 };
            var currencyItem25 = new Currency() { Number = "752", Code = "SEK", Name = "Шведская крона", Amount = 1 };

            var currencyItem26 = new Currency() { Number = "702", Code = "SGD", Name = "Сингапурский доллар", Amount = 1 };
            var currencyItem27 = new Currency() { Number = "764", Code = "THB", Name = "Тайский Бат", Amount = 100 };
            var currencyItem28 = new Currency() { Number = "949", Code = "TRY", Name = "Турецкая лира", Amount = 1 };
            var currencyItem29 = new Currency() { Number = "840", Code = "USD", Name = "Доллар США", Amount = 1 };
            var currencyItem30 = new Currency() { Number = "960", Code = "XDR", Name = "СДР (специальные права заимствования)", Amount = 1 };

            var currencyItem31 = new Currency() { Number = "710", Code = "ZAR", Name = "Рэнд", Amount = 1 };
            var currencyItem32 = new Currency() { Number = "124", Code = "CAD", Name = "Канадский доллар", Amount = 1 };
            var currencyItem33 = new Currency() { Number = "356", Code = "INR", Name = "Индийская рупия", Amount = 100 };
            #endregion  Заполнение справочника валют
            modelBuilder.Entity<Currency>().HasData(new[] { 
                currencyItem1,currencyItem2,currencyItem3,currencyItem4,currencyItem5,
                currencyItem6,currencyItem7,currencyItem8,currencyItem9,currencyItem10,

                currencyItem11,currencyItem12,currencyItem13,currencyItem14,currencyItem15,
                currencyItem16,currencyItem17,currencyItem18,currencyItem19,currencyItem20,

                currencyItem21,currencyItem22,currencyItem23,currencyItem24,currencyItem25,
                currencyItem26,currencyItem27,currencyItem28,currencyItem29,currencyItem30,
                
                currencyItem31,currencyItem32,currencyItem33

            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
