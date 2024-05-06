using Microsoft.EntityFrameworkCore.Storage;

namespace BigDinner.Application.Common.Abstractions.Repository;

public interface IUnitOfWork : IDisposable
{
    IDbContextTransaction BeginTransaction();

    Task<int> CompleteAsync();
}


