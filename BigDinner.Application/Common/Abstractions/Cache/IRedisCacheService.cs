namespace BigDinner.Application.Common.Abstractions.Cache;

public interface IRedisCacheService
{
    Task<T> Get<T>(string key, Func<Task<T>> GetFromDB);

    Task<T> GetAsycn<T>(string key);

    Task Invalidate(string key);

    Task SetAsync<T>(string key,T notifications, TimeSpan expirDuration); // Set expiration time
}
