
using System.Threading.Tasks;
using CzechCurrency.API.Requests;
using CzechCurrency.Entities;
using CzechCurrency.Events;
using CzechCurrency.Services.Contract;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CzechCurrency.API.Controllers
{
    /// <summary>
    /// Контроллер курсов обмена
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ExchangeRateController : ControllerBase
    {
        private readonly IExchangeRateService _exchangeRateService;
        private readonly IBusControl _busControl;

        private readonly ILogger<ExchangeRateController> _logger;

        public ExchangeRateController(ILogger<ExchangeRateController> logger,
            IExchangeRateService exchangeRateService,
            IBusControl busControl)
        {
            _logger = logger;
            _exchangeRateService = exchangeRateService;
            _busControl = busControl;
        }

        [HttpGet("Get")]
        public async Task<ActionResult<ExchangeRate>> Get([FromQuery] ExchangeRateRequest exchangeRateRequest)
        {

            var result = await _exchangeRateService.Get(exchangeRateRequest.CurrencyCode, exchangeRateRequest.Date);
            if (result == null)
            {
                return NotFound("Курс обмена не найден");
            }
            await _busControl.Publish(
                new ExchangeRateReportEvent(exchangeRateRequest.Date.AddMonths(-1), exchangeRateRequest.Date, exchangeRateRequest.CurrencyCode));

            return Ok(result);
        }
    }
}
