namespace BigDinner.Domain.Models.Base;

public class PaginatedResult<T>
{
    public PaginatedResult(IEnumerable<T> data)
    {
        Data = data;
    }
    public IEnumerable<T> Data { get; set; }

    public PaginatedResult(bool succeeded, IEnumerable<T> data = default, IEnumerable<string> messages = null, int count = 0, int page = 1, int pageSize = 10)
    {
        Data = data;
        CurrentPage = page;
        Succeeded = succeeded;
        PageSize = pageSize;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        TotalCount = count;
    }

    public static PaginatedResult<T> Success(IEnumerable<T> data, int count, int page, int pageSize)
    {
        return new(true, data, null, count, page, pageSize);
    }

    public int CurrentPage { get; set; }

    public int TotalPages { get; set; }

    public int TotalCount { get; set; }

    public object Meta { get; set; }

    public int PageSize { get; set; }

    public bool HasPreviousPage => CurrentPage > 1;

    public bool HasNextPage => CurrentPage < TotalPages;

    public List<string> Messages { get; set; } = new();

    public bool Succeeded { get; set; }
}
