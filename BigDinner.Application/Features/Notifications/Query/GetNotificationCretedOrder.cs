
using BigDinner.Application.Common.Abstractions.Cache;
using BigDinner.Application.Features.Orders.Query;
using BigDinner.Domain.Models.Notifications;
using BigDinner.Domain.Models.Orders;

namespace BigDinner.Application.Features.Notifications.Query;

public record GetNotificationQuery()
    : IRequest<Response<IEnumerable<GetNotificationQueryResponse>>>;

public record GetNotificationQueryResponse(Guid CustomerId, string Title, string Message, DateTimeOffset CreatedAt, bool IsRead);

public sealed class GetNotificationQueryHandler : ResponseHandler,
    IRequestHandler<GetNotificationQuery, Response<IEnumerable<GetNotificationQueryResponse>>>
{
    private readonly IRedisCacheService _cache;
    private readonly IMapper _mapper;

    public GetNotificationQueryHandler(IRedisCacheService cache, IMapper mapper)
    {
        _cache = cache;
        _mapper = mapper;
    }

    public async Task<Response<IEnumerable<GetNotificationQueryResponse>>> Handle(GetNotificationQuery request, CancellationToken cancellationToken)
    {
        var result= await _cache.GetAsycn<IEnumerable<Notification>>("notifications");

        var mapped = _mapper.Map<IEnumerable<GetNotificationQueryResponse>>(result);

        return Success<IEnumerable<GetNotificationQueryResponse>>(mapped);
    }
}


public class GetOrderQueryProfile : Profile
{
    public GetOrderQueryProfile()
    {
        CreateMap<Notification, GetNotificationQueryResponse>();

    }
}