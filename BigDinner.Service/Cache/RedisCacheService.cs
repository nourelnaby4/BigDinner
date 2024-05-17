using BigDinner.Application.Common.Abstractions.Cache;
using BigDinner.Application.Common.Abstractions.JsonSerialize;
using StackExchange.Redis;
using System.Text.Json;
namespace BigDinner.Service.Cache;

public class RedisCacheService : IRedisCacheService
{
    public static ConnectionMultiplexer Connection => lazyConnection.Value;

     /** use Lazy connection to laze connectio creation and set cuuncreency
     mode for multi threading **/
    private static Lazy<ConnectionMultiplexer> lazyConnection =
        new Lazy<ConnectionMultiplexer>(() =>
        {
            return ConnectionMultiplexer.Connect("localhost:6379");
        });

    private readonly JsonSerializerOptions _jsonSerializerOptions;

    IDatabase _cache;

    public RedisCacheService(
        JsonSerializerOptions jsonSerializerOptions)
    {
        _cache = Connection.GetDatabase();
        _jsonSerializerOptions = jsonSerializerOptions;
    }


    public async Task<T> Get<T>(string key, Func<Task<T>> GetFromDB)
    {
        T result;

        if (_cache.KeyExists(key))
        {
            var cacheData =await _cache.StringGetAsync(key);

            result = JsonSerializer.Deserialize<T>(cacheData, _jsonSerializerOptions);
        }
        else
        {
            result = await GetFromDB();

            await _cache.StringSetAsync(key, JsonSerializer.Serialize(result, _jsonSerializerOptions),TimeSpan.FromMinutes(30));
        }

        return result;
    }

    public async Task Invalidate(string key)
    {
        await _cache.KeyDeleteAsync(key);
    }

}
