namespace BigDinner.Application.Common.Abstractions.Cache;

public interface IMemoryCacheService
{
    bool TryGetValue<T>(string key, out T result);

    Task<T> Get<T>(string key, Func<Task<T>> GetFromDB);

    void Set<T>(string key, T value);

    Task<T> Get<T>(string key);

    void Update<T>(string key, T value);

    Task Remove(string key);
}
