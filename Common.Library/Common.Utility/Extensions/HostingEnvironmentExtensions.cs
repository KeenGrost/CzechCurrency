using Common.Utility.Host;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System;

namespace Common.Utility.Extensions
{
    public static class HostingEnvironmentExtensions
    {
        /// <summary>
        /// Инициализация окружения <see cref="IHostEnvironment"/>>
        /// </summary>
        /// <param name="hostingEnvironment"></param>
        /// <param name="contentRootPath">Корневой путь приложения</param>
        /// <param name="options">Опции приложения-утилиты</param>
        public static void Initialize(this IHostEnvironment hostingEnvironment, string contentRootPath,
            UtilityHostOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            hostingEnvironment.ApplicationName = options.ApplicationName;
            hostingEnvironment.ContentRootPath = contentRootPath;
            hostingEnvironment.ContentRootFileProvider = new PhysicalFileProvider(hostingEnvironment.ContentRootPath);

            hostingEnvironment.EnvironmentName = options.Environment ?? hostingEnvironment.EnvironmentName;
        }
    }
}
