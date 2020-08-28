using System;
using System.Threading.Tasks;
using Common.Service;
using CzechCurrency.Entities;
using JetBrains.Annotations;

namespace CzechCurrency.Services.Contract
{

    /// <summary>
    /// Интерфейс сервиса справочника валют
    /// </summary>
    public interface ICurrencyService : IBaseService<Currency>
    {
        /// <summary>
        /// Получить валюту по коду
        /// </summary>
        /// <param name="code">Код</param>
        /// <returns></returns>
        [ItemNotNull]
        Task<Currency> GetByCode([NotNull] string code);

        /// <summary>
        /// Получить валюту по номеру
        /// </summary>
        /// <param name="number">Номер</param>
        /// <returns></returns>
        [ItemNotNull]
        Task<Currency> GetByNumber([NotNull] string number);
        
    }
}
