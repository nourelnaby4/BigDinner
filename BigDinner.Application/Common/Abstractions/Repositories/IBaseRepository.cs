
using Microsoft.EntityFrameworkCore;

namespace BigDinner.Application.Common.Abstractions.Repositories;

public interface IBaseRepo<T> where T : class
{
    Task<T> GetByIdAsync(string id);
    Task<IEnumerable<T>> GetAsync();
    Task<T> AddAsync(T entity);
    T Update(T entity);
    void Delete(T entity);
    Task<ICollection<T>> AddRangeAsync(ICollection<T> entities);
    void DeleteRange(ICollection<T> entities);
    void UpdateRange(ICollection<T> entities);

    Task<int> SaveChangesAsync();
}

