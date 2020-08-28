using Common.Utility.Host;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Utility.Extensions
{
    public static class UtilityHostBuilderExtension
    {
        /// <summary>
        /// Использовать Startup для конфигурирования приложения
        /// </summary>
        /// <typeparam name="TStartup">Startup</typeparam>
        /// <param name="hostBuilder">Builder приложения</param>
        /// <returns></returns>
        [PublicAPI]
        public static IUtilityHostBuilder UseStartup<TStartup>(this IUtilityHostBuilder hostBuilder)
            where TStartup : class, IUtilityStartup
        {
            return hostBuilder.ConfigureServices(services =>
            {
                services.AddSingleton<IUtilityStartup, TStartup>();
                hostBuilder.UseStartup(services);
            });
        }
    }
}
