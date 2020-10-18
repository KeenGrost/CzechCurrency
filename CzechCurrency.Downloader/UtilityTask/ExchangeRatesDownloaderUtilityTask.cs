using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using Common.Utility;
using CzechCurrency.Downloader.Options;
using CzechCurrency.Services.Contract;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using CzechCurrency.Entities;
using CzechCurrency.Services.Contract.Api;

namespace CzechCurrency.Downloader.UtilityTask
{
    /// <summary>
    /// Загрузчик курсов валют
    /// </summary>
    public class ExchangeRatesDownloaderUtilityTask : IUtilityTask
    {

        private readonly ICnbApi _cnbApi;
        private readonly IExchangeRateService _exchangeRateService;
        private readonly ICurrencyService _currencyService;
        private readonly ILogger<CzechCurrencyDownloaderOptions> _logger;
        private readonly CzechCurrencyDownloaderOptions _czechCurrencyDownloaderOptions;


        public ExchangeRatesDownloaderUtilityTask(
            IExchangeRateService exchangeRateService,
            ILogger<CzechCurrencyDownloaderOptions> logger,
            IOptionsMonitor<CzechCurrencyDownloaderOptions> czechCurrencyDownloaderOptions, 
            ICnbApi cnbApi, 
            ICurrencyService currencyService)
        {
            _exchangeRateService = exchangeRateService;
            _logger = logger;
            _cnbApi = cnbApi;
            _currencyService = currencyService;
            _czechCurrencyDownloaderOptions = czechCurrencyDownloaderOptions.CurrentValue;
        }

        public async Task Execute()
        {
            await ImportExchangeRates();
        }

        private async Task ImportExchangeRates()
        {
            // Скачать файл
            //
            var exchangeRatesSource = await _cnbApi.ExchangeRates(_czechCurrencyDownloaderOptions.Year);
            var contentStream = await exchangeRatesSource.ReadAsStreamAsync(); // get the actual content stream

            // Распарсить файл
            //
            List<ExchangeRate> exchangeRates  = await ParsingFile(contentStream);

            // Пакетно удалим текущие записи за год, чтоб не было дублирования
            await _exchangeRateService.DeleteByYear(_czechCurrencyDownloaderOptions.Year);

            // Добавить в БД
            await _exchangeRateService.AddRange(exchangeRates);

            _logger.LogInformation("Файл Импортирован в БД");
        }

        private async Task<List<ExchangeRate>> ParsingFile(Stream contentStream)
        {
            string[] currencies = null;
            List<ExchangeRate> exchangeRates = new List<ExchangeRate>();
            string fieldDate = "Date";
            string cellSeparator = "|";
            string headerSeparator = " ";

            using var streamReader = new StreamReader(contentStream);
            while (!streamReader.EndOfStream)
            {
                string line = await streamReader.ReadLineAsync();
                if (line == null) continue;

                string[] exchangeRatesParams = line.Trim().Split(cellSeparator);

                if (currencies == null && exchangeRatesParams[0] == fieldDate)
                {
                    currencies = exchangeRatesParams.Skip(1).ToArray();
                    // Проверка, что все валюты есть в справочнике. Иначе будет ошибка внешнего ключа.
                    //
                    Currency[] existCurrencies = await _currencyService.GetAll();
                    foreach (string item in currencies)
                    {
                        if (!existCurrencies.Where(x => x.Code == item).ToArray().Any())
                        {
                            throw new Exception($"В справочнике не найдена валюта {item}.");
                        }
                    }
                    continue;
                }



                if (currencies == null) break;

                string data = exchangeRatesParams[0];
                exchangeRatesParams = exchangeRatesParams.Skip(1).ToArray();
                for (int i = 0; i < exchangeRatesParams.Length; i++)
                {
                    string currencyCode = currencies[i].Split(headerSeparator)[1];
                    exchangeRates.Add(ExchangeRate.CreateFromImportFile(exchangeRatesParams[i], currencyCode, data));
                }
            }

            return exchangeRates;
        }
    }
}
