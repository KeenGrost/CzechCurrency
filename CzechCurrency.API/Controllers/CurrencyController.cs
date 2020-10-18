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
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyService _currencyService;

        private readonly ILogger<CurrencyController> _logger;

        public CurrencyController(
            ILogger<CurrencyController> logger,
            ICurrencyService currencyService)
        {
            _logger = logger;
            _currencyService = currencyService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<Currency[]>> GetAll()
        {
            _logger.LogInformation("валюты");
            var result = await _currencyService.GetAll();
            return Ok(result);
        }
    }
}
