namespace BigDinner.Application.Common.Abstractions.Cache;

public interface IMemoryCacheService
{
    Task<T> Get<T>(string key, Func<Task<T>> GetFromDB);

    Task Invalidate(string key);
}
