using System;
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
using CzechCurrency.Services.Consumers;
using CzechCurrency.Services.Options;
using GreenPipes;
using MassTransit;

namespace CzechCurrency.API
{
    public class Startup
    {
        private const string ConfigurationConnectionStringCzechCurrency = "DbConfig:DbConnectionStrings:CzechCurrency";

        private const string ConfigurationConnectionStringRedis = "Redis:ConnectionString";

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

            services.AddControllers();

            services.AddSwaggerDocument(settings => { settings.Title = "CzechCurrency API"; });

            RegisterDistributedCache(services);

            RegisterRabbitMq(services);
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

        /// <summary>
        /// Регистрируем брокер сообщений
        /// </summary>
        /// <param name="services"></param>
        private void RegisterRabbitMq(IServiceCollection services)
        {
            var rabbitOptions = new RabbitMqOptions();
            Configuration.GetSection("RabbitMqOptions").Bind(rabbitOptions);

            services.AddMassTransit(configurator =>
            {
                // MassTransit нативно поддерживает наш DI.
                configurator.AddConsumer<ExchangeRateReportConsumer>();

                configurator.AddBus(context => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    cfg.UseHealthCheck(context);

                    cfg.Host(new Uri(rabbitOptions.Url), hostConfigurator =>
                    {
                        hostConfigurator.Username(rabbitOptions.Login);
                        hostConfigurator.Password(rabbitOptions.Password);
                    });

                    // Используем встроенный планировщик в RabbitMQ
                    cfg.UseDelayedExchangeMessageScheduler();

                    // Регистрация очереди из которой будем получать сообщения
                    cfg.ReceiveEndpoint("CzechCurrencyApiQueue", ep =>
                    {
                        // При неудачно завершении задачи (а так же после отработки политики повторов,
                        // так же не удачных если они включены) задача будет выпущена с задержкой
                        ep.UseScheduledRedelivery(ret =>
                        {
                            // Можно ловить определенные исключения
                            //ret.Handle<TimeoutException>();

                            // Можно игнорировать некоторые исключения, перепоставки не будет, а сразу в Fault очередь
                            ret.Ignore(typeof(ArgumentException));

                            ret.Intervals(TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(15), TimeSpan.FromMinutes(30));
                        });

                        // Политика повторов пытается выполнить одну и ту же задачу несколько раз не снимая с неё блокировки.
                        // Политику повтора можно использовать с политикой повторной доставки совместно.
                        // Большой таймаут ставить - плохое решение.
                        //ep.UseMessageRetry(r =>
                        //{
                        //    r.Interval(2, TimeSpan.FromSeconds(10));
                        //});

                        // Объявляем потребителя события для формирования отчета
                        ep.ConfigureConsumer<ExchangeRateReportConsumer>(context);

                        // Можно регистрировать несколько потребителей
                        // ep.ConfigureConsumer<AnotherConsumer>(provider);
                    });
                }));
            });

            services.AddMassTransitHostedService();
        }

        private void RegisterDistributedCache(IServiceCollection services)
        {
            if (Environment.IsDevelopment())
            {
                services.AddDistributedMemoryCache();
            }
            else
            {
                services.AddStackExchangeRedisCache(option =>
                {
                    option.Configuration = Configuration[ConfigurationConnectionStringRedis];
                    option.InstanceName = "czech:";
                });
            }
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

            app.UseCors(builder => builder.WithOrigins("http://localhost:4200").AllowAnyHeader());

            app.UseOpenApi();

            app.UseSwaggerUi3();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
