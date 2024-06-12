using BigDinner.Domain.Models.Orders;

namespace BigDinner.Application.Features.Orders.Query;

public record GetOrderPaginatedListQuery() : IRequest<Response<PaginatedResult<GetOrderQueryResposne>>>;

public class GetOrderPaginatedListQueryHandler : ResponseHandler,
    IRequestHandler<GetOrderPaginatedListQuery, Response<PaginatedResult<GetOrderQueryResposne>>>
{
    private readonly IMapper _mapper;
    private readonly IOrderRepository _orderRepository;

    public GetOrderPaginatedListQueryHandler(
        IMapper mapper,
        IOrderRepository orderRepository)
    {
        _mapper = mapper;
        _orderRepository = orderRepository;
    }

    public async Task<Response<PaginatedResult<GetOrderQueryResposne>>> Handle(GetOrderPaginatedListQuery request, CancellationToken cancellationToken)
    {
        var model = await _orderRepository.GetPaginationList();

        var response = _mapper.Map<PaginatedResult<GetOrderQueryResposne>>(model);

        return Success<PaginatedResult<GetOrderQueryResposne>>(response);
    }
}