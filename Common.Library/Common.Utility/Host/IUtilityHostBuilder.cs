using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Common.Utility.Host
{
    /// <summary>
    /// Builder для IUtilityHost
    /// </summary>
    [PublicAPI]
    public interface IUtilityHostBuilder
    {
        /// <summary>
        /// Добавляет делегат для конфигурации приложения
        /// </summary>
        /// <param name="configure">Делегат конфигурации. Входные параметры <see cref="IServiceCollection"/> - коллекция сервисов</param>
        /// <returns></returns>
        IUtilityHostBuilder ConfigureServices(Action<IServiceCollection> configure);

        /// <summary>
        /// Добавляет делегат для конфигурации приложения
        /// </summary>
        /// <param name="configure"> Делегат конфигурации. Входные параметры <see cref="UtilityHostBuilderContext"/> - контекст builder's , <see cref="IServiceCollection"/> - коллекция сервисов</param>
        /// <returns></returns>
        IUtilityHostBuilder ConfigureServices(Action<UtilityHostBuilderContext, IServiceCollection> configure);

        /// <summary>
        /// Добавить делегат для конфигурирования <seealso cref="IConfigurationBuilder"/> который построит <see cref="IConfiguration"/>
        /// </summary>
        /// <param name="configureDelegate"></param>
        /// <returns><see cref="IConfiguration"/></returns>
        IUtilityHostBuilder ConfigureAppConfiguration(Action<UtilityHostBuilderContext, IConfigurationBuilder> configureDelegate);

        /// <summary>
        /// Использовать Startup
        /// </summary>
        /// <param name="services"></param>
        void UseStartup(IServiceCollection services);
    }
}
