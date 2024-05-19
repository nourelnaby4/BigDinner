using BigDinner.Application.Common.Abstractions.Cache;
using BigDinner.Application.Common.Abstractions.JsonSerialize;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using StackExchange.Redis;
namespace BigDinner.Service.Cache;

public class RedisCacheService : IRedisCacheService
{
    private ConnectionMultiplexer Connection;

    /** use Lazy connection to laze connectio creation and set cuuncreency
    mode for multi threading **/

    private static Lazy<ConnectionMultiplexer> lazyConnection;

    private readonly JsonSerializerSettings _jsonSerializerOptions;

    IDatabase _cache;

    public RedisCacheService(
        JsonSerializerSettings jsonSerializerOptions,
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

            result = JsonConvert.DeserializeObject<T>(cacheData, new JsonSerializerSettings
            {
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                ContractResolver = new PrivateResolver(),
            });
        }
        else
        {
            result = await GetFromDB();

            await _cache.StringSetAsync(key, JsonConvert.SerializeObject(result, new JsonSerializerSettings
            {
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                ContractResolver = new PrivateResolver(),
            }), TimeSpan.FromMinutes(30));
        }

        return result;
    }

    public async Task Invalidate(string key)
    {
        if (_cache.KeyExists(key))
        {
            await _cache.KeyDeleteAsync(key);
        }
    }
}
