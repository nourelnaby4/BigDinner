using BigDinner.Domain.Models.Base;
using BigDinner.Domain.Models.Orders;
using BigDinner.Persistence.Extensions;
using BigDinner.Test.Persistense.Wrapper.interfaces;

namespace BigDinner.Test.Persistense.Wrapper.Impelmentations;

public class PaginatedService : IPaginated<Order>
{
    public async Task<PaginatedResult<Order>> ToPaginatedListAsync(IQueryable<Order> source, int pageNumber=1, int pageSize = 10)
    {
       return await source.ToPaginatedListAsync(pageNumber, pageSize);
    }
}
