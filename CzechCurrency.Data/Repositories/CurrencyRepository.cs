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
    /// Репозиторий логов активированных пакетов
    /// </summary>
    public class CurrencyRepository : BaseRepository<Currency>, ICurrencyRepository
    {
        public CurrencyRepository(CzechCurrencyDbContext context) : base(context)
        {
        }

        public Task<Currency> GetByCode(string code)
        {
            throw new NotImplementedException();
        }

        public Task<Currency> GetByNumber(string number)
        {
            throw new NotImplementedException();
        }
    }
}

