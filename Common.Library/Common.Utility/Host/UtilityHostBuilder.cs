using Common.Utility.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using System;
using System.IO;
using System.Reflection;

namespace Common.Utility.Host
{
    /// <summary>
    /// Builder для IUtilityHost
    /// </summary>
    public class UtilityHostBuilder : IUtilityHostBuilder
    {
        private readonly IHostBuilder _builder;

        public UtilityHostBuilder(IHostBuilder builder)
        {
            _builder = builder;

            IConfiguration config = new ConfigurationBuilder()
                .AddEnvironmentVariables("ASPNETCORE_")
                .Build();

            _builder.UseContentRoot(Directory.GetCurrentDirectory());

            _builder.ConfigureHostConfiguration(configurationBuilder =>
                {
                    configurationBuilder.AddConfiguration(config);
                });

            _builder.ConfigureServices((context, services) =>
            {
                var utilityHostContext = GetUtilityHostBuilderContext(context);
                services.AddSingleton(utilityHostContext.HostingEnvironment);
            });
        }


        public void UseStartup(IServiceCollection services)
        {
            var serviceProvider = GetProviderFromFactory(services);
            var startup = serviceProvider.GetService<IUtilityStartup>();
            startup.ConfigureServices(services);
        }

        public IUtilityHostBuilder ConfigureServices(Action<IServiceCollection> configure)
        {
            return ConfigureServices((_, services) => configure(services));
        }

        public IUtilityHostBuilder ConfigureServices(Action<UtilityHostBuilderContext, IServiceCollection> configureServices)
        {
            _builder.ConfigureServices((context, services) =>
            {
                var utilityContext = GetUtilityHostBuilderContext(context);
                configureServices(utilityContext, services);
            });

            return this;
        }

        /// <summary>
        /// Добавить делегат для конфигурирования <see cref="IConfigurationBuilder"> </see>, который построит <see cref="IConfiguration"/>
        /// </summary>
        /// <param name="configureDelegate">Делегат для конфигурирования <see cref="IConfigurationBuilder"> </see>, который построит <see cref="IConfiguration"/></param>
        /// <returns></returns>
        public IUtilityHostBuilder ConfigureAppConfiguration(Action<UtilityHostBuilderContext, IConfigurationBuilder> configureDelegate)
        {
            _builder.ConfigureAppConfiguration((context, builder) =>
            {
                var utilityContext = GetUtilityHostBuilderContext(context);
                configureDelegate(utilityContext, builder);
            });

            return this;
        }

        /// <summary>
        /// Получить провайдер сервисов
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        private static IServiceProvider GetProviderFromFactory(IServiceCollection collection)
        {
            var provider = collection.BuildServiceProvider();
            var factory = provider.GetService<IServiceProviderFactory<IServiceCollection>>();

            if (factory != null && !(factory is DefaultServiceProviderFactory))
            {
                using (provider)
                {
                    return factory.CreateServiceProvider(factory.CreateBuilder(collection));
                }
            }

            return provider;
        }

        /// <summary>
        /// Получить контекст Builder'a
        /// <remarks>Происходит инициализация компонентов</remarks>
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private static UtilityHostBuilderContext GetUtilityHostBuilderContext(HostBuilderContext context)
        {
            if (!context.Properties.TryGetValue(typeof(UtilityHostBuilderContext), out var contextVal))
            {
                var options = new UtilityHostOptions(context.Configuration, Assembly.GetEntryAssembly()?.GetName().Name);
                var utilityHostBuilderContext = new UtilityHostBuilderContext
                {
                    Configuration = context.Configuration,
                    HostingEnvironment = new HostingEnvironment(),
                };
                utilityHostBuilderContext.HostingEnvironment.Initialize(context.HostingEnvironment.ContentRootPath,
                    options);
                context.Properties[typeof(UtilityHostBuilderContext)] = utilityHostBuilderContext;
                context.Properties[typeof(UtilityHostOptions)] = options;
                return utilityHostBuilderContext;
            }

            var utilityHostContext = (UtilityHostBuilderContext)contextVal;
            utilityHostContext.Configuration = context.Configuration;
            return utilityHostContext;
        }
    }
}
