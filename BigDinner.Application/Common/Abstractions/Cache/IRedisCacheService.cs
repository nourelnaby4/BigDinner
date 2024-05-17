namespace BigDinner.Application.Common.Abstractions.Cache;

public interface IRedisCacheService
{
    Task<T> Get<T>(string key, Func<Task<T>> GetFromDB);

    Task Invalidate(string key);
}
