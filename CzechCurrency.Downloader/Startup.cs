using Common.Utility;
using Common.Utility.Extensions;
using CzechCurrency.Data;
using CzechCurrency.Data.Contract;
using CzechCurrency.Data.Repositories;
using CzechCurrency.Downloader.Options;
using CzechCurrency.Downloader.UtilityTask;
using CzechCurrency.Services;
using CzechCurrency.Services.Contract;
using CzechCurrency.Services.Contract.Api;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Polly;
using Polly.Extensions.Http;
using Refit;
using Serilog;
using System;
using System.Net.Http;

namespace CzechCurrency.Downloader
{
    public class Startup : IUtilityStartup
    {
        private IConfiguration Configuration { get; }

        /// <summary>
        /// ctor
        /// </summary>
        public Startup(IHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddJsonFile("serilog.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"serilog.{env.EnvironmentName}.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .CreateLogger();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container
        /// </summary>
        /// <param name="service"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            RegisterApi(services);
            RegisterLogging(services);
            RegisterServices(services);
            RegisterRepositories(services);
            RegisterBackgroundTasks(services);
            RegisterOptions(services);

            // Конфигурация БД контекста
            string configurationConnectionString = Configuration["DbConfig:DbConnectionStrings:CzechCurrency"];

            services.AddDbContext<CzechCurrencyDbContext>(options => options.UseNpgsql(configurationConnectionString));
        }

        /// <summary>
        /// Регистрация сервиса для логирования
        /// </summary>
        /// <param name="services"></param>
        private void RegisterLogging(IServiceCollection services)
        {
            // Serilog for DI
            services.AddLogging(loggingBuilder =>
                loggingBuilder.AddSerilog(dispose: true));
        }

        private void RegisterApi(IServiceCollection services)
        {
            var cnbApiOptions = new CnbApiOptions();
            Configuration.GetSection("CnbApiOptions").Bind(cnbApiOptions);

            services.AddRefitClient<ICnbApi>()
                .ConfigureHttpClient(c =>
                {
                    c.BaseAddress = new Uri(cnbApiOptions.Server);
                    c.Timeout = TimeSpan.FromSeconds(cnbApiOptions.TimeoutSec);
                })
                .SetHandlerLifetime(TimeSpan.FromMinutes(5))
                .AddPolicyHandler(GetRetryPolicy());
        }

        private void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<ICurrencyService, CurrencyService>();
            services.AddScoped<IExchangeRateService, ExchangeRateService>();
        }

        private void RegisterRepositories(IServiceCollection services)
        {
            services.AddScoped<ICurrencyRepository, CurrencyRepository>();
            services.AddScoped<IExchangeRateRepository, ExchangeRateRepository>();
        }

        private void RegisterOptions(IServiceCollection services)
        {
            services.AddOptions();
            services.Configure<CzechCurrencyDownloaderOptions>(
                Configuration.GetSection("CzechCurrencyDownloaderOptions"));
        }

        private void RegisterBackgroundTasks(IServiceCollection services)
        {
            var utilityOptions = new UtilityOptions();
            Configuration.Bind(nameof(UtilityOptions), utilityOptions);

            if (utilityOptions.RunFlag.HasValue)
            {
                if ((utilityOptions.RunFlag & UtilityRunFlags.DOWNLOAD) != 0)
                {
                    services.AddUtility<ExchangeRatesDownloaderUtilityTask>();
                }
            }
            else
            {
                throw new InvalidOperationException($"Не найдена опция {nameof(UtilityOptions)}");
            }
        }

        /// <summary>
        /// Базовая политика повторов с колебанием задержки
        /// <remarks>Политики, заданные с помощью этого метода расширения, обрабатывают HttpRequestException,
        /// ответы HTTP 5xx и ответы HTTP 408, 429</remarks>
        /// </summary>
        private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            var jitterer = new Random();

            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
                .WaitAndRetryAsync(3, sleep => TimeSpan.FromSeconds(1)
                                               + TimeSpan.FromMilliseconds(jitterer.Next(0, 100)));
        }
    }
}
