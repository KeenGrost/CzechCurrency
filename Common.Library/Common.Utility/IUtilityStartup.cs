using Microsoft.Extensions.DependencyInjection;

namespace Common.Utility
{
    /// <summary>
    /// Интерфейс Startup
    /// </summary>
    public interface IUtilityStartup
    {
        /// <summary>
        /// Конфигурирование сервисов
        /// </summary>
        /// <param name="service"></param>
        void ConfigureServices(IServiceCollection service);
    }
}
