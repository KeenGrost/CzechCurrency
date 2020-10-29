using System;

namespace CzechCurrency.Events
{
    /// <summary>
    /// Событие генерации отчета
    /// </summary>
    public interface IExchangeRateReportEvent
    {
        /// <summary>
        /// Дата начала периода отчета
        /// </summary>
        DateTime StartDate { get; }

        /// <summary>
        /// Дата окончания периода отчета
        /// </summary>
        DateTime EndDate { get; }

        /// <summary>
        /// Код валюты по которой формируется отчет
        /// </summary>
        string CurrencyCode { get; }
    }
}
