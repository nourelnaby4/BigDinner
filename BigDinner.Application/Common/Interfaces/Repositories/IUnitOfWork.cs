using Microsoft.EntityFrameworkCore.Storage;

namespace BigDinner.Persistence.Repository;

public interface IUnitOfWork<T> : IDisposable where T : class
{
    IBaseRepo<T> Entity { get; }
    IDbContextTransaction BeginTransaction();
    Task<int> CompleteAsync();
}


