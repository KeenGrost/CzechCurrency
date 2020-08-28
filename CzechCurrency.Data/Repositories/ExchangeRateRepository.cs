using System;
using System.Linq;
using System.Threading.Tasks;
using Common.Data.EF;
using CzechCurrency.Data.Contract;
using CzechCurrency.Entities;
using Microsoft.EntityFrameworkCore;

namespace CzechCurrency.Data.Repositories
{

    /// <summary>
    /// Репозиторий курсов обменов валют
    /// </summary>
    public class ExchangeRateRepository : BaseRepository<ExchangeRate>, IExchangeRateRepository
    {
        public ExchangeRateRepository(CzechCurrencyDbContext context) : base(context)
        {
        }


        public async Task<ExchangeRate> Get(string currencyCode, DateTime date)
        {
            var exchangeRate = await DbSet
                .Where(x => x.CurrencyCode == currencyCode && x.Date == date.Date)
                .OrderBy(x => x.Date)
                .AsNoTracking()
                .LastOrDefaultAsync();
            return exchangeRate;
        }
    }
}
