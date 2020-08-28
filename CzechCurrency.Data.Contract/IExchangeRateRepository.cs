using System;
using System.Threading.Tasks;
using Common.Data.Repository;
using CzechCurrency.Entities;
using JetBrains.Annotations;

namespace CzechCurrency.Data.Contract
{
    /// <summary>
    /// Интерфейс репозитория истории курса валют
    /// </summary>
    public interface IExchangeRateRepository : IBaseRepository<ExchangeRate>
    {
        /// <summary>
        /// Получить курс обмена по дате и по коду валюты
        /// </summary>
        /// <param name="currencyCode">Код</param>
        /// <param name="date"></param>
        /// <returns>История активаций пакетов</returns>
        [CanBeNull]
        Task<ExchangeRate> Get([NotNull] string currencyCode, DateTime date);

    }
}
