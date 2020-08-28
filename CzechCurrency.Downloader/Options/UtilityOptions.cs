using System;
using System.Collections.Generic;
using System.Text;

namespace CzechCurrency.Downloader.Options
{
    /// <summary>
    /// Опции конфигурации утилиты
    /// </summary>
    public class UtilityOptions
    {
        public  UtilityRunFlags? RunFlag { get; set; }
    }


    [Flags]
    public enum UtilityRunFlags
    {
        /// <summary>
        /// Загрузка
        /// </summary>
        DOWNLOAD = 1
    }
}
