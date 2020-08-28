namespace CzechCurrency.Downloader.Options
{
    /// <summary>
    /// Конфигурация и параметры для Cnb Api
    /// </summary>
    public class CnbApiOptions
    {
        /// <summary>
        /// Адрес сервера
        /// </summary>
        public string Server { get; set; }

        /// <summary>
        /// Таймаут в секундах
        /// </summary>
        public int TimeoutSec { get; set; }
    }
}
