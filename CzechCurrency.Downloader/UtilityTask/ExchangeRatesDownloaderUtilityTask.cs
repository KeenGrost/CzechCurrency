using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        private readonly ILogger<CzechCurrencyDownloaderOptions> _logger;
        private readonly CzechCurrencyDownloaderOptions _czechCurrencyDownloaderOptions;


        public ExchangeRatesDownloaderUtilityTask(
            IExchangeRateService exchangeRateService,
            ILogger<CzechCurrencyDownloaderOptions> logger,
            IOptionsMonitor<CzechCurrencyDownloaderOptions> czechCurrencyDownloaderOptions, 
            ICnbApi cnbApi)
        {
            _exchangeRateService = exchangeRateService;
            _logger = logger;
            _cnbApi = cnbApi;
            _czechCurrencyDownloaderOptions = czechCurrencyDownloaderOptions.CurrentValue;
        }

        public async Task Execute()
        {
            await DownloadExchageRates();
        }

        private async Task DownloadExchageRates()
        {
            var exchangeRates = new List<ExchangeRate>();

            //Шапка файла
            string[] currencies = null;

            // Скачать файл
            //
            var exchangeRatesSource = await _cnbApi.ExchangeRates(_czechCurrencyDownloaderOptions.Year);
            var contentStream = await exchangeRatesSource.ReadAsStreamAsync(); // get the actual content stream

            // Распарсить файл
            //
            using (var streamReader = new StreamReader(contentStream))
            {
                while (!streamReader.EndOfStream)
                {

                    var line = streamReader.ReadLine();
                    if (line == null) continue;

                    var exchangeRatesParams = line.Trim().Split("|");
                    
                    if (currencies == null && exchangeRatesParams[0] == "Date")
                    {
                        currencies = exchangeRatesParams.Skip(1).ToArray();
                        //todo проверка, что все валюты есть в справочнике. Иначе будет ошибка внешнего ключа.
                        
                        continue;
                    }

                    if (currencies == null) break;

                    string data = exchangeRatesParams[0];
                    exchangeRatesParams = exchangeRatesParams.Skip(1).ToArray();
                    for (int i = 0; i < exchangeRatesParams.Length; i++)
                    {
                        string currencyCode = currencies[i].Split(" ")[1];
                        exchangeRates.Add(ExchangeRate.CreateFromImportFile(exchangeRatesParams[i], currencyCode, data));
                    }
                    

                   
                }
            }
           

            // Добавить в БД
            await _exchangeRateService.AddRange(exchangeRates);
            _logger.LogInformation("Файл Импортирован в БД");
        }
    }
}
