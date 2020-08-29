using System;
using System.Threading.Tasks;
using Common.Data.Repository;
using CzechCurrency.Entities;
using JetBrains.Annotations;

namespace CzechCurrency.Data.Contract
{
    /// <summary>
    /// Интерфейс репозитория справочника валют
    /// </summary>
    public interface ICurrencyRepository : IBaseRepository<Currency>
    {

        /// <summary>
        /// Получить валюту по коду
        /// </summary>
        /// <param name="code">Код</param>
        /// <returns>Валюта</returns>
        [ItemNotNull]
        Task<Currency> GetByCode([NotNull]string code);

        /// <summary>
        /// Получить валюту по номеру
        /// </summary>
        /// <param name="number">Номер</param>
        /// <returns>Валюта</returns>
        [ItemNotNull]
        Task<Currency> GetByNumber([NotNull] string number);

    }
}
