using System;
using System.Collections.Generic;
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
    public class ExchangeRateService : BaseService<ExchangeRate>, IExchangeRateService
    {
        private readonly IExchangeRateRepository _repository;

        public ExchangeRateService(IExchangeRateRepository repository) : base(repository)
        {
            _repository = repository;
        }


        public Task<ExchangeRate> Get(string numberCurrency, DateTime date)
        {
            throw new NotImplementedException();
        }

        public async Task AddRange(IEnumerable<ExchangeRate> exchangeRates)
        {
            await _repository.AddRange(exchangeRates);
        }

        public async Task DeleteByYear(int year)
        {
            ExchangeRate[] exchangeRates = await GetByYear(year);
            await _repository.DeleteRange(exchangeRates);
        }

        public async Task<ExchangeRate[]> GetByYear(int year)
        {
            return await _repository.GetAll(x=>x.Date.Year == year);
        }
    }
}
