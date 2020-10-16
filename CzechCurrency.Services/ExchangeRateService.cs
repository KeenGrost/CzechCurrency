using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Common.Extensions;
using Common.Service;
using CzechCurrency.Data.Contract;
using CzechCurrency.Entities;
using CzechCurrency.Services.Contract;
using Microsoft.Extensions.Caching.Distributed;

namespace CzechCurrency.Services
{
    /// <summary>
    /// Сервис работы со справочником валют
    /// </summary>
    public class ExchangeRateService : BaseService<ExchangeRate>, IExchangeRateService
    {

        private const string CacheKey = "ExchangeRate";
        private static readonly TimeSpan cacheExpiration = TimeSpan.FromHours(1);
        private static readonly SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);

        private readonly IDistributedCache _cache;
        private readonly IExchangeRateRepository _repository;



        public ExchangeRateService(IExchangeRateRepository repository, IDistributedCache cache) : base(repository)
        {
            _repository = repository;
            _cache = cache;
        }


        public async Task<ExchangeRate> Get(string currencyCode, DateTime date)
        {

            var cacheKey = CacheKey + currencyCode + date;
            ExchangeRate exchangeRate = _cache.GetObject<ExchangeRate>(cacheKey);
            if (exchangeRate != null)
            {
                return exchangeRate;
            }

            await semaphore.WaitAsync();

            try
            {
                DateTime minimalDate = new DateTime(1990, 12, 31);
                while (exchangeRate == null && date > minimalDate)
                {
                    exchangeRate = await _repository.Get(currencyCode, date);
                    // смещение с учетом

                    date = date.AddDays(-1);
                }

                _cache.SetObject(cacheKey, exchangeRate, cacheExpiration);

                return exchangeRate;
            }
            finally
            {
                semaphore.Release();
            }
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
            return await _repository.GetAll(x => x.Date.Year == year);
        }
    }
}
