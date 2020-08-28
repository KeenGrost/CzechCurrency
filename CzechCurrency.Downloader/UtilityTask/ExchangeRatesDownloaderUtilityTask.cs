using Common.Utility;
using CzechCurrency.Downloader.Options;
using CzechCurrency.Services.Contract;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace CzechCurrency.Downloader.UtilityTask
{
    /// <summary>
    /// Загрузчик курсов валют
    /// </summary>
    public class ExchangeRatesDownloaderUtilityTask : IUtilityTask
    {
        private readonly IExchangeRateService _exchangeRateService;
        private readonly ILogger<CzechCurrencyDownloaderOptions> _logger;
        private readonly CzechCurrencyDownloaderOptions _czechCurrencyDownloaderOptions;


        public ExchangeRatesDownloaderUtilityTask(
            IExchangeRateService exchangeRateService,
            ILogger<CzechCurrencyDownloaderOptions> logger,
            IOptionsMonitor<CzechCurrencyDownloaderOptions> czechCurrencyDownloaderOptions)
        {
            _exchangeRateService = exchangeRateService;
            _logger = logger;
            _czechCurrencyDownloaderOptions = czechCurrencyDownloaderOptions.CurrentValue;
        }

        public async Task Execute()
        {
            await DownloadExchageRates();
        }

        private async Task DownloadExchageRates()
        {
            //todo Скачать файл
            _logger.LogInformation("Файл Скачан");

            //todo распарсить файл
            _logger.LogInformation("Файл Распознан");

            //todo вставить в БД addRange
            _logger.LogInformation("Файл Импортирован в БД");
        }
    }
}
