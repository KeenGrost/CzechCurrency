using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CzechCurrency.Entities
{

    /// <summary>
    /// Справочник международных валют в соответствии ISO 4217 
    /// </summary>
    [Table("currencies")]
    public class Currency
    {

        /// <summary>
        /// Код
        /// </summary>
        [Key]
        [Column("code")]
        public string Code { get; set; }

        /// <summary>
        /// Цифровой трехзначный номер
        /// </summary>
        [Column("number")]
        public string Number { get; set; }


        /// <summary>
        /// Наименование валюты
        /// </summary>
        [Column("name")]
        public string Name { get; set; }

        /// <summary>
        /// Количество валюты
        /// </summary>
        [Column("amount")]
        public int Amount { get; set; }
    }
}
