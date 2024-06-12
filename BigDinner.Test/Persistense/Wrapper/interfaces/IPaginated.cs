using BigDinner.Domain.Models.Base;

namespace BigDinner.Test.Persistense.Wrapper.interfaces;

public interface IPaginated<T>
{
    public Task<PaginatedResult<T>> ToPaginatedListAsync( IQueryable<T> source, int pageNumber=1, int pageSize = 10);
}
