using KT.Application;
using KT.Domain.Exceptions;
using Microsoft.Extensions.Caching.Memory;

namespace KT.Infrastructure.Data.Cache
{
    public class CacheService : ICacheService
    {
        MemoryCache _memoryCache = new MemoryCache(new MemoryCacheOptions());

        public bool TryToGetData<T>(string key)
        {
            if (_memoryCache.TryGetValue(key, out T _))
                return true;
            return false;
        }
        public T GetData<T>(string key)
        {
            try
            {
                T item = (T)_memoryCache.Get(key);
                return item;
            }
            catch
            {
                throw new TryAgainException();
            }
        }
        public bool SetData<T>(string key, T value, DateTimeOffset expirationTime)
        {
            bool res = true;
            try
            {
                if (!string.IsNullOrEmpty(key))
                {
                    _memoryCache.Set(key, value, expirationTime);
                }
            }
            catch
            {
                throw new TryAgainException();
            }
            return res;
        }
        public bool RemoveData(string key)
        {
            try
            {
                if (!string.IsNullOrEmpty(key))
                {
                    _memoryCache.Remove(key);
                    return true;
                }
            }
            catch
            {
                throw new TryAgainException();
            }
            return false;
        }
    }
}