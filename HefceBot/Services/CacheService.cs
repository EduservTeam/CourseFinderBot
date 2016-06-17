using System;
using System.Runtime.Caching;

namespace HefceBot.Services
{
    public class CacheService : ICacheService
    {
        public T GetOrSet<T>(string cacheKey, Func<T> getItemCallback) where T : class
        {
            var item = MemoryCache.Default.Get(cacheKey) as T;
            if (item == null)
            {
                item = getItemCallback();
                MemoryCache.Default.Add(cacheKey, item, DateTime.Now.AddMinutes(10));
            }
            return item;
        }

        public T GetOrSet<T>(string className, string methodName, string skipVal, Func<T> getItemCallback) where T : class
        {
            return GetOrSet($"{methodName}_{skipVal}", getItemCallback);
        }
    }

    public interface ICacheService
    {
        T GetOrSet<T>(string cacheKey, Func<T> getItemCallback) where T : class;
        T GetOrSet<T>(string className, string methodName, string skipVal, Func<T> getItemCallback) where T : class;
    }
}