using System;

namespace CzechCurrency.Events
{
    /// <summary>
    /// Событие генерации отчета
    /// </summary>
    public class ExchangeRateReportEvent : IExchangeRateReportEvent
    {
        public ExchangeRateReportEvent(DateTime startDate, DateTime endDate, string code)
        {
            StartDate = startDate;
            EndDate = endDate;
            CurrencyCode = code;
        }

        public DateTime StartDate { get; private set; }

        public DateTime EndDate { get; private set; }

        public string CurrencyCode { get; private set; }
    }
}
