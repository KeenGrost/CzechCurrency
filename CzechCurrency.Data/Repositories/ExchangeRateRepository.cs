using System;
using System.Threading.Tasks;
using Common.Data.EF;
using CzechCurrency.Data.Contract;
using CzechCurrency.Entities;

namespace CzechCurrency.Data.Repositories
{

    /// <summary>
    /// Репозиторий логов активированных пакетов
    /// </summary>
    public class ExchangeRateRepository : BaseRepository<ExchangeRate>, IExchangeRateRepository
    {
        public ExchangeRateRepository(CzechCurrencyDbContext context) : base(context)
        {
        }

        public Task<Currency> Get(string numberCurrency, DateTime date)
        {
            throw new NotImplementedException();
        }
    }
}
