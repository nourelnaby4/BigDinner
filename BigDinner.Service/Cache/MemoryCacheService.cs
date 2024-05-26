using BigDinner.Application.Common.Abstractions.Cache;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

namespace BigDinner.Service.Cache;

public class MemoryCacheService : IRedisCacheService
{
    private readonly IMemoryCache _memoryCache;

    public MemoryCacheService(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public async Task<T> Get<T>(string key, Func<Task<T>> GetFromDB)
    {
        if (!_memoryCache.TryGetValue(key, out T result))
        {
            result = await GetFromDB();
            _memoryCache.Set(key, result);
        }

        await  Task.CompletedTask;

        return result;
    }
   
    public async Task Invalidate(string key)
    {
         _memoryCache.Remove(key);

        await Task.CompletedTask;
    }
}
