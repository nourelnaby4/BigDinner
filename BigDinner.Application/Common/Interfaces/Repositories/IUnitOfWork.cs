using Microsoft.EntityFrameworkCore.Storage;

namespace BigDinner.Persistence.Repository;

public interface IUnitOfWork : IDisposable
{
    IDbContextTransaction BeginTransaction();

    Task<int> CompleteAsync();
}


