using JetBrains.Annotations;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;

namespace Common.Extensions
{
    /// <summary>
    /// Класс расширений для <see cref="IDistributedCache"/>
    /// </summary>
    public static class DistributedCacheExtensions
    {
        /// <summary>
        /// Поместить объект в кэш
        /// </summary>
        /// <typeparam name="T">Тип объекта</typeparam>
        /// <param name="cache">Интерфейс распределённого кэша</param>
        /// <param name="key">Ключ</param>
        /// <param name="value">Помещаемый объект</param>
        /// <param name="expiration">Время истечения кэша относительно текущего момента</param>
        public static void SetObject<T>(this IDistributedCache cache, [NotNull] string key, [NotNull] T value, TimeSpan expiration)
        {
            try
            {
                var json = JsonConvert.SerializeObject(value);
                cache.SetString(key, json, new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = expiration
                });
            }
            catch
            {
                //ignore
            }
        }

        /// <summary>
        /// Поместить объект в кэш
        /// </summary>
        /// <typeparam name="T">Тип объекта</typeparam>
        /// <param name="cache">Интерфейс распределённого кэша</param>
        /// <param name="key">Ключ</param>
        /// <param name="value">Помещаемый объект</param>
        /// <param name="expiration">Время истечения кэша относительно текущего момента</param>
        /// <param name="jsonMaxDepth">Максимальная глубина вложенности для JSON</param>
        public static void SetObject<T>(this IDistributedCache cache, [NotNull] string key, [NotNull] T value, TimeSpan expiration,
            int jsonMaxDepth)
        {
            try
            {
                var json = JsonConvert.SerializeObject(value, new JsonSerializerSettings { MaxDepth = jsonMaxDepth });
                cache.SetString(key, json, new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = expiration
                });
            }
            catch
            {
                //ignore
            }
        }

        /// <summary>
        /// Поместить объект в кэш
        /// </summary>
        /// <typeparam name="T">Тип объекта</typeparam>
        /// <param name="cache">Интерфейс распределённого кэша</param>
        /// <param name="key">Ключ</param>
        /// <param name="value">Помещаемый объект</param>
        /// <param name="expiration">Абсолютная дата истечения срока действия для записи кэша</param>
        public static void SetObject<T>(this IDistributedCache cache, [NotNull] string key, [NotNull] T value, DateTime expiration)
        {
            try
            {
                var json = JsonConvert.SerializeObject(value);
                cache.SetString(key, json, new DistributedCacheEntryOptions
                {
                    AbsoluteExpiration = expiration
                });
            }
            catch
            {
                //ignore
            }
        }

        /// <summary>
        /// Поместить объект в кэш
        /// </summary>
        /// <typeparam name="T">Тип объекта</typeparam>
        /// <param name="cache">Интерфейс распределённого кэша</param>
        /// <param name="key">Ключ</param>
        /// <param name="value">Помещаемый объект</param>
        /// <param name="cacheOptions">Опции распределённого кэша</param>
        public static void SetObject<T>(this IDistributedCache cache, [NotNull] string key, [NotNull] T value,
            DistributedCacheEntryOptions cacheOptions)
        {
            try
            {
                var json = JsonConvert.SerializeObject(value);
                cache.SetString(key, json, cacheOptions);
            }
            catch
            {
                //ignore
            }
        }

        /// <summary>
        /// Получить объект из кэша
        /// </summary>
        /// <typeparam name="T">Тип объекта</typeparam>
        /// <param name="cache">Интерфейс распределённого кэша</param>
        /// <param name="key">Ключ</param>
        /// <returns></returns>
        [CanBeNull]
        public static T GetObject<T>(this IDistributedCache cache, [NotNull] string key)
        {
            try
            {
                var json = cache.GetString(key);
                return json != null ? JsonConvert.DeserializeObject<T>(json) : default(T);
            }
            catch
            {
                return default(T);
            }
        }
    }
}
