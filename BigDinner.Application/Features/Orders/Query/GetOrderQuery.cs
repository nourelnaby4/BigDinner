using BigDinner.Domain.Models.Orders;

namespace BigDinner.Application.Features.Orders.Query;

public record GetOrderQuery() : IRequest<Response<IEnumerable<GetOrderQueryResposne>>>;

public record GetOrderQueryResposne()
{
    public Guid OrderId { get; set; }
    public string OrderStatus { get; set; }
    public string ShippingStatus { get; set; }
    public Price TotalPrice { get; set;}
    public DateTime OrderDateOnUtc { get; set; }
}

public sealed class GetOrderQueryHandler : ResponseHandler,
    IRequestHandler<GetOrderQuery, Response<IEnumerable<GetOrderQueryResposne>>>
{
    private readonly IMapper _mapper;

    private readonly IOrderRepository _orderRepository;

    private readonly IUnitOfWork _unitOfWork;

    public GetOrderQueryHandler(
        IMapper mapper,
        IOrderRepository orderRepository,
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<IEnumerable<GetOrderQueryResposne>>> Handle(GetOrderQuery request, CancellationToken cancellationToken)
    {
        var model = await _orderRepository.GetAsync();

        var response = _mapper.Map<IEnumerable<GetOrderQueryResposne>>(model);

        return Success<IEnumerable<GetOrderQueryResposne>>(response);
    }
}

public class GetOrderQueryProfile : Profile
{
    public GetOrderQueryProfile()
    {
        CreateMap<Order, GetOrderQueryResposne>()
            .ForMember(desc => desc.OrderId, option => option.MapFrom(src => src.Id))
            .ForMember(desc => desc.OrderStatus, option => option.MapFrom(src => src.OrderStatus.ToString()))
            .ForMember(desc => desc.ShippingStatus, option => option.MapFrom(src => src.Shipping.Status.ToString()))
            .ForMember(desc => desc.TotalPrice, option => option.MapFrom(src => src.CalculateTotalPrice()));
    }
}
