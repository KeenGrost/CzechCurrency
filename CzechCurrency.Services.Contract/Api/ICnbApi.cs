using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Refit;

namespace CzechCurrency.Services.Contract.Api
{
    /// <summary>
    /// Интерфейс клиента Чешский национального банка
    /// </summary>
    public interface ICnbApi
    {
        /// <summary>
        /// Поиск городов по имени
        /// </summary>
        /// <param name="year">фильтрация выборки по году</param>
        /// <returns>Список регионов доступных для заказа</returns>
        [Get("/en/financial_markets/foreign_exchange_market/exchange_rate_fixing/year.txt?year={year}")]
        Task<HttpContent> ExchangeRates(int year);
    }
}
