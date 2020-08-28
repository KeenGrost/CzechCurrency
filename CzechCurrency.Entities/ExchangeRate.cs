using System;
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

        /// <summary>
        /// Создать курс обмена из файла импорта
        /// </summary>
        public static ExchangeRate CreateFromImportFile([NotNull] string[] param)
        {
            //todo доделать
            return new ExchangeRate
            {
                Value = "123",
                CurrencyNumber = "840",
                Date = DateTime.ParseExact(param[0], "dd.MM.yyyy", CultureInfo.InvariantCulture)
            };
        }
    }
}
