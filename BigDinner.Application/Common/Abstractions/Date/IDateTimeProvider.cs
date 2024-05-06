
namespace BigDinner.Application.Common.Abstractions.Date;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}
