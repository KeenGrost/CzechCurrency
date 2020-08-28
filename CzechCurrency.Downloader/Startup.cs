using System;
using Common.Service;
using System.Text;
using Common.Utility;
using Common.Utility.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CzechCurrency.Data;
using CzechCurrency.Data.Contract;
using CzechCurrency.Data.Repositories;
using CzechCurrency.Services;
using CzechCurrency.Services.Contract;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Globalization;
using CzechCurrency.Downloader.Options;
using CzechCurrency.Downloader.UtilityTask;
using Microsoft.EntityFrameworkCore.Internal;


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
           var builder = new  ConfigurationBuilder()
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

            RegisterServices(services);
            RegisterRepositories(services);
            RegisterBackgroundTasks(services);
            RegisterOptions(services);

            // Конфигурация БД контекста
            string configurationConnectionString = Configuration["DbConfig:DbConnectionString:CzechCurrency"];

            services.AddDbContext<CzechCurrencyDbContext>(options => options.UseNpgsql(configurationConnectionString));
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
            Configuration.Bind(nameof(UtilityOptions),utilityOptions);

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
    }
}
