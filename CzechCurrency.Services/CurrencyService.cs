using System;
using System.Threading.Tasks;
using Common.Service;
using CzechCurrency.Data.Contract;
using CzechCurrency.Entities;
using CzechCurrency.Services.Contract;

namespace CzechCurrency.Services
{
    /// <summary>
    /// Сервис работы со справочником валют
    /// </summary>
    public class CurrencyService : BaseService<Currency>, ICurrencyService
    {
        private readonly ICurrencyRepository _repository;

        public CurrencyService(ICurrencyRepository repository) : base(repository)
        {
            _repository = repository;
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
