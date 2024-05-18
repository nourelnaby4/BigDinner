using BigDinner.Application.Common.Abstractions.Cache;
using BigDinner.Application.Common.Abstractions.JsonSerialize;
using BigDinner.Domain.Models.Notifications;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace BigDinner.Service.Cache;

public class RedisCacheService : IRedisCacheService
{
    private ConnectionMultiplexer Connection;

    /** use Lazy connection to laze connectio creation and set cuuncreency
    mode for multi threading **/

    private static Lazy<ConnectionMultiplexer> lazyConnection;

    private readonly JsonSerializerOptions _jsonSerializerOptions;

    IDatabase _cache;

    public RedisCacheService(
        JsonSerializerOptions jsonSerializerOptions,
        RedisSetting redisSetting)
    {
        lazyConnection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(redisSetting.ConnectionString));
        Connection = lazyConnection.Value;
        _cache = Connection.GetDatabase();
        _jsonSerializerOptions = jsonSerializerOptions;
    }


    public async Task<T> Get<T>(string key, Func<Task<T>> GetFromDB)
    {
        T result;

        if (_cache.KeyExists(key))
        {
            var cacheData = await _cache.StringGetAsync(key);

            result = await GetAsycn<T>(key);
        }
        else
        {
            result = await GetFromDB();

            await SetAsync<T>(key, result, TimeSpan.FromMinutes(30));
        }

        return result;
    }

    public async Task<T> GetAsycn<T>(string key)
    {
        var cacheData = await _cache.StringGetAsync(key);

        T result = JsonSerializer.Deserialize<T>(cacheData, _jsonSerializerOptions);

        return result;
    }

    public async Task Invalidate(string key)
    {
        await _cache.KeyDeleteAsync(key);
    }

    public async Task SetAsync<T>(string key, T data, TimeSpan expirDuration)
    {
        await _cache.StringSetAsync(key, JsonSerializer.Serialize(data, _jsonSerializerOptions), expirDuration);
    }
}