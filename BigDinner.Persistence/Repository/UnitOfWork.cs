using BigDinner.Application.Common.Abstractions.Repository;
using Microsoft.EntityFrameworkCore.Storage;

namespace BigDinner.Persistence.Repository;

public class UnitOfWork : IUnitOfWork 
{
    private readonly ApplicationDbContext _context;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }
   
    public IDbContextTransaction BeginTransaction()
    {
        return _context.Database.BeginTransaction();
    }

    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}

