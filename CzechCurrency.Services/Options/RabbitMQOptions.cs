namespace CzechCurrency.Services.Options
{
    /// <summary>
    /// Конфигурация RabbitMQ
    /// </summary>
    public class RabbitMqOptions
    {
        /// <summary>
        /// Адрес брокера сообщений
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Логин
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; }
    }
}
