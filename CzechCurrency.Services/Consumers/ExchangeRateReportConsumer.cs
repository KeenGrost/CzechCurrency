using System.Threading;
using MassTransit;
using System.Threading.Tasks;
using CzechCurrency.Events;
using Microsoft.Extensions.Logging;

namespace CzechCurrency.Services.Consumers
{
    public class ExchangeRateReportConsumer : IConsumer<IExchangeRateReportEvent>
    {
        private readonly ILogger<ExchangeRateReportConsumer> _logger;
        public ExchangeRateReportConsumer(ILogger<ExchangeRateReportConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<IExchangeRateReportEvent> context)
        {
            _logger.LogInformation($"Стартуем создание отчета");

            await Task.Delay(context.Message.CurrencyCode.Length * 1000);

            _logger.LogInformation($"Отчет создан");

        }
    }
}
