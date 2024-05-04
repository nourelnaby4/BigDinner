using Microsoft.EntityFrameworkCore.Storage;

namespace BigDinner.Persistence.Repository;

public class UnitOfWork<T> : IUnitOfWork<T> where T : class
{
    private readonly DinnerDbContext _context;
    private IBaseRepo<T> _entity;
    public UnitOfWork(DinnerDbContext context)
    {
        _context = context;
    }
    public IBaseRepo<T> Entity
    {
        get
        {
            return _entity ?? (_entity = new BaseRepo<T>(_context));
        }
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

