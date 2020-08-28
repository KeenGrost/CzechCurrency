using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace CzechCurrency.Entities
{

    /// <summary>
    /// История курсов обмена
    /// </summary>
    [Table("exchange_rates")]
    public class ExchangeRate
    {

        /// <summary>
        /// ID записи.
        /// </summary>
        [Column("id")]
        public long Id { get; set; }

        /// <summary>
        /// Код валюты
        /// </summary>
        [Column("code")]
        public string CurrencyCode { get; set; }


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
        public decimal Value { get; set; }

        /// <summary>
        /// Создать курс обмена из файла импорта
        /// </summary>
        public static ExchangeRate CreateFromImportFile([NotNull] string value, [NotNull] string currencyCode, string data)
        {
            return new ExchangeRate
            {
                Value = Decimal.Parse(value.Replace(',', '.'), NumberStyles.Float,
                    CultureInfo.InvariantCulture),
                CurrencyCode = currencyCode,
                Date = DateTime.ParseExact(data, "dd.MM.yyyy", CultureInfo.InvariantCulture)
            };
        }
    }
}
