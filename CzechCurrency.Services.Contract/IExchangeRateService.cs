using System;
using System.Threading.Tasks;
using Common.Service;
using CzechCurrency.Entities;
using JetBrains.Annotations;

namespace CzechCurrency.Services.Contract
{
    /// <summary>
    /// Интерфейс истории курсов валют
    /// </summary>
    public interface IExchangeRateService : IBaseService<ExchangeRate>
    {
        /// <summary>
        /// Получить курс обмена по дате и по коду валюты
        /// </summary>
        /// <param name="numberCurrency">Код</param>
        /// <param name="date">Дата</param>
        /// <returns></returns>
        [ItemNotNull]
        Task<ExchangeRate> Get([NotNull] string numberCurrency, DateTime date);

     

    }
}
