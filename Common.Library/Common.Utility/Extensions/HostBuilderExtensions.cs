using Common.Utility.Host;
using JetBrains.Annotations;
using Microsoft.Extensions.Hosting;
using System;

namespace Common.Utility.Extensions
{
    public static class HostBuilderExtensions
    {
        /// <summary>
        /// Конфигурировать UtilityHost по умолчанию
        /// </summary>
        /// <param name="builder">Builder  хоста</param>
        /// <param name="configure">Делегат конфигурации</param>
        /// <returns></returns>
        [PublicAPI]
        public static IHostBuilder ConfigureUtilityHostDefaults(this IHostBuilder builder, Action<IUtilityHostBuilder> configure)
        {
            var utilityHostBuilder = new UtilityHostBuilder(builder);
            configure(utilityHostBuilder);
            return builder;
        }
    }
}
