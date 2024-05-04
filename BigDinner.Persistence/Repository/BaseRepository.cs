namespace BigDinner.Persistence.Repository;

public class BaseRepo<T> : IBaseRepo<T> where T : class
{
    protected readonly ApplicationDbContext _context;

    public BaseRepo(ApplicationDbContext context)
    {
        _context = context;
    }

    public virtual async Task<T> GetByIdAsync(string id)
    {

        return await _context.Set<T>().FindAsync(id);
    }
    public virtual async Task<IEnumerable<T>> GetAsync()
    {

        return await _context.Set<T>().ToListAsync();
    }
    public virtual async Task<T> AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);


        return entity;
    }

    public virtual T Update(T entity)
    {
        _context.Set<T>().Update(entity);
        return entity;

    }
    public virtual void Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
    }
   
    public virtual async Task<ICollection<T>> AddRangeAsync(ICollection<T> entities)
    {
        await _context.Set<T>().AddRangeAsync(entities);
        return entities;
    }

    public void UpdateRange(ICollection<T> entities)
    {
        _context.Set<T>().UpdateRange(entities);
    }

    public void DeleteRange(ICollection<T> entities)
    {
        foreach (var entity in entities)
        {
            _context.Entry(entity).State = EntityState.Deleted;
        }
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}

