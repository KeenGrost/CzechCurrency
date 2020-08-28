using Configuration;
using CzechCurrency.Data;
using CzechCurrency.Data.Contract;
using CzechCurrency.Data.Repositories;
using CzechCurrency.Services;
using CzechCurrency.Services.Contract;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.Globalization;

namespace CzechCurrency.API
{
    public class Startup
    {
        private const string ConfigurationConnectionStringCzechCurrency = "DbConfig:DbConnectionStrings:CzechCurrency";


        public IConfiguration Configuration { get; }

        /// <summary>
        /// Environment
        /// </summary>
        public IWebHostEnvironment Environment { get; }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="env"></param>
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddJsonFile("serilog.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"serilog.{env.EnvironmentName}.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();

            Environment = env;
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .CreateLogger();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var cultureInfo = new CultureInfo("ru-RU");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            services.AddSingleton(new ConfigurationManager(Configuration));

            string conString = Configuration[ConfigurationConnectionStringCzechCurrency];

            services.AddDbContext<CzechCurrencyDbContext>(options =>
                options.UseNpgsql(conString, b =>
                {
                    b.MigrationsAssembly("CzechCurrency.Data.Migrations");
                }));

            services.AddDbContext<CzechCurrencyDbContext>(options => options.UseNpgsql(conString));

            RegisterServices(services);

            RegisterRepositories(services);

            // RegisterSwagger(services);

            services.AddControllers();
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

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
