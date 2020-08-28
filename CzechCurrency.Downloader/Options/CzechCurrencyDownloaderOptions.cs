namespace CzechCurrency.Downloader.Options
{
    public class CzechCurrencyDownloaderOptions
    {
        /// <summary>
        /// Адрес расположения данных
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Фильтр выборки по годам
        /// </summary>
        public string Year { get; set; }
    }
}
