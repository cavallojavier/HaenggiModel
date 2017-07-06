using System;
using System.Configuration;
using System.Runtime.Caching;

namespace HaenggiModel.CalculationHelper.Cache
{
    public static class CacheHelper
    {
        private static MemoryCache cache = new MemoryCache("DentalApplication");
        private static readonly int expirationTime = int.Parse(ConfigurationManager.AppSettings["CacheExpirationTime"]);

        /// <summary>
        /// Insert value into the cache using
        /// appropriate name/value pairs
        /// </summary>
        /// <typeparam name="T">Type of cached item</typeparam>
        /// <param name="cachingObject">Item to be cached</param>
        /// <param name="key">Name of item</param>
        public static void Add<T>(T cachingObject, string key)
        {
            Clear(key);

            cache.Add(
                key,
                cachingObject,
                DateTime.Now.AddMinutes(expirationTime));
        }

        /// <summary>
        /// Remove item from cache
        /// </summary>
        /// <param name="key">Name of cached item</param>
        public static void Clear(string key)
        {
            cache.Remove(key);
        }

        /// <summary>
        /// Check for item in cache
        /// </summary>
        /// <param name="key">Name of cached item</param>
        /// <returns></returns>
        public static bool Exists(string key)
        {
            return cache.GetCacheItem(key) != null;
        }

        /// <summary>
        /// Retrieve cached item
        /// </summary>
        /// <typeparam name="T">Type of cached item</typeparam>
        /// <param name="key">Name of cached item</param>
        /// <param name="value">Cached value. Default(T) if item doesn't exist.</param>
        /// <returns>Cached item as type</returns>
        public static bool Get<T>(string key, out T value)
        {
            try
            {
                if (!Exists(key))
                {
                    value = default(T);
                    return false;
                }

                value = (T)cache.GetCacheItem(key).Value;
            }
            catch
            {
                value = default(T);
                return false;
            }

            return true;
        }
    }
}
