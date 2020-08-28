using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Utility.Extensions
{
    public static class HostExtensions
    {
        /// <summary>
        /// Запуск утилит
        /// </summary>
        /// <param name="host">Хост приложения</param>
        /// <returns></returns>
        [PublicAPI]
        public static async Task RunUtility(this IHost host)
        {
            try
            {
                using (var scope = host.Services.CreateScope())
                {
                    var utilities = GetUtilities(scope);
                    foreach (var utilityTask in utilities)
                    {
                        await utilityTask.Execute();
                    }
                }
            }
            catch (Exception ex)
            {
                var logger = GetLogger(host);

                logger.LogError(ex, "Произошла ошибка при работе Utility");
                throw;
            }
        }

        /// <summary>
        /// Получить все утилиты
        /// </summary>
        /// <param name="scope">scope приложения</param>
        /// <returns></returns>
        private static IEnumerable<IUtilityTask> GetUtilities(IServiceScope scope)
        {
            return scope.ServiceProvider.GetService<IEnumerable<IUtilityTask>>();
        }

        /// <summary>
        /// Получить логгер
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        private static ILogger<IHost> GetLogger(IHost host)
        {
            return host.Services.GetService<ILogger<IHost>>();
        }
    }
}
