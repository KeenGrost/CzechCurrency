using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CzechCurrency.Entities
{

    /// <summary>
    /// История курсов обмена
    /// </summary>
    [Table("exchange_rates")]
    public class ExchangeRate
    {

        /// <summary>
        /// Код валюты
        /// </summary>
        [Column("currency_number")]
        public string CurrencyNumber { get; set; }

        public Currency Currency { get; set; }

        /// <summary>
        /// Дата
        /// </summary>
        [Column("date")]
        public DateTime Date { get; set; }


        /// <summary>
        /// Значение курса
        /// </summary>
        [Column("value")]
        public string Value { get; set; }
    }
}
