using BigDinner.Application.Common.Models;

namespace BigDinner.Persistence.Extensions;

public static class IQueryableExtenstion
{
    public static async Task<PaginatedResult<T>> ToPaginatedListAsync<T>(this IQueryable<T> source, int pageNumber, int pageSize) where T : class
    {
        if (source == null)
            throw new ArgumentNullException("Source is null");

        pageNumber = pageNumber == 0 ? 1 : pageNumber;

        pageSize = pageSize == 0 ? 10 : pageSize;

        int count = await source.AsNoTracking().CountAsync();

        if (count == 0)
            return PaginatedResult<T>.Success(new List<T>(), count, pageNumber, pageSize);

        pageNumber = pageNumber <= 0 ? 1 : pageNumber;

        var items = await source.Skip((pageNumber - 1) * pageSize)
                                .Take(pageSize)
                                .ToListAsync();

        return PaginatedResult<T>.Success(items, count, pageNumber, pageSize);
    }
}
