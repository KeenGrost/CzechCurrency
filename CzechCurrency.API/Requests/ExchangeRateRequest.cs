using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CzechCurrency.API.Requests
{
    /// <summary>
    /// Запрос на получение курса обмена валют
    /// </summary>
    public class ExchangeRateRequest
    {
        /// <summary>
        /// Трехбуквенный код валюты
        /// </summary>
        public string CurrencyCode { get; set; }

        private DateTime _date;
        /// <summary>
        /// Дата, на которую нужно получить курс обмена
        /// </summary>
        public DateTime Date
        {
            get
            {
                if (_date >= DateTime.UtcNow.Date)
                {
                    return DateTime.UtcNow.Date;
                }
                return _date;
            }
            set => _date = value;
        }
    }
}
