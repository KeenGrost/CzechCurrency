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


        public async Task<ExchangeRate> Get(string currencyCode, DateTime date)
        {
            DateTime minimalDate = new DateTime(1990,12,31);
            ExchangeRate exchangeRate = null;
            while (exchangeRate == null && date > minimalDate)
            {
                exchangeRate = await _repository.Get(currencyCode, date);
                // смещение с учетом

                date = date.AddDays(-1);
            }
            return exchangeRate;
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
