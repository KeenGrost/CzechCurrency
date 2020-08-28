using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Common.Utility.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Добавить задачу утилиту для выполнения
        /// </summary>
        /// <typeparam name="TUtilityTask">Утилита для выполнения в приложении</typeparam>
        /// <param name="services">Сервисы</param>
        /// <returns></returns>
        [PublicAPI]
        public static IServiceCollection AddUtility<TUtilityTask>(this IServiceCollection services)
            where TUtilityTask : class, IUtilityTask
        {
            services.TryAddEnumerable(ServiceDescriptor.Scoped<IUtilityTask, TUtilityTask>());
            return services;
        }
    }
}
