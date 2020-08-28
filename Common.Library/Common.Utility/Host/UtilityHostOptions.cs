using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;

namespace Common.Utility.Host
{
    public class UtilityHostOptions
    {
        public UtilityHostOptions()
        {
        }

        public UtilityHostOptions(IConfiguration configuration)
            : this(configuration, string.Empty)
        {
        }

        public UtilityHostOptions(IConfiguration configuration, string applicationNameFallback)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            ApplicationName = configuration[WebHostDefaults.ApplicationKey] ?? applicationNameFallback;
            StartupAssembly = configuration[WebHostDefaults.StartupAssemblyKey];
            Environment = configuration[WebHostDefaults.EnvironmentKey];

            ContentRootPath = configuration[WebHostDefaults.ContentRootKey];
        }

        /// <summary>
        /// Название приложения
        /// </summary>
        public string ApplicationName { get; set; }

        /// <summary>
        /// Тип окружения приложения.
        /// <remarks>Development, Production, Staging</remarks>
        /// </summary>
        public string Environment { get; set; }

        /// <summary>
        /// Сборка Startup
        /// </summary>
        public string StartupAssembly { get; set; }

        /// <summary>
        /// Корневой путь приложения
        /// </summary>
        public string ContentRootPath { get; set; }
    }
}
