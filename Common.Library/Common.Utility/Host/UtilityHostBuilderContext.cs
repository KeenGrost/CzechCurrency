using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Common.Utility.Host
{
    /// <summary>
    /// Контекст builder's хранит <see cref="IHostEnvironment"/> b <see cref="IConfiguration"/>
    /// </summary>
    public class UtilityHostBuilderContext
    {
        /// <summary>
        /// Информация об окружении приложения
        /// </summary>
        public IHostEnvironment HostingEnvironment { get; set; }

        /// <summary>
        /// Хранит конфигурацию приложения
        /// </summary>
        public IConfiguration Configuration { get; set; }
    }
}
