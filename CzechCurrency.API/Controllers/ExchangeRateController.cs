using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using CzechCurrency.API.Requests;
using CzechCurrency.Entities;
using CzechCurrency.Services.Contract;
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

        private readonly ILogger<ExchangeRateController> _logger;

        public ExchangeRateController(ILogger<ExchangeRateController> logger, IExchangeRateService exchangeRateService)
        {
            _logger = logger;
            _exchangeRateService = exchangeRateService;
        }

        [HttpGet("Get")]
        public async Task<ActionResult<ExchangeRate>> Get([FromQuery]ExchangeRateRequest exchangeRateRequest)
        {
            _logger.LogInformation("курсы обмена");

            var result = await  _exchangeRateService.Get(exchangeRateRequest.CurrencyCode, exchangeRateRequest.Date);
            if (result == null)
            {
                return NotFound("Курс обмена не найден");
            }

            return Ok(result);
        }
    }
}
