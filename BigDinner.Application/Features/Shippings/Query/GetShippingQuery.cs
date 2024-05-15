using BigDinner.Domain.Models.Shippings;

namespace BigDinner.Application.Features.Shippings.Query;


public record GetShippingQuery() :
    IRequest<Response<IEnumerable<GetShippingQueryResponse>>>;

public record GetShippingQueryResponse(Guid OrderId, Address Address, string Status, string ShippingMethodName);

public sealed class GetShippingQueryHandler : ResponseHandler,
    IRequestHandler<GetShippingQuery, Response<IEnumerable<GetShippingQueryResponse>>>
{
    private readonly IMapper _mapper;

    private readonly IShippingRepository _shippingRepo;

    public GetShippingQueryHandler(IMapper mapper, IShippingRepository shippingRepo)
    {
        _mapper = mapper;
        _shippingRepo = shippingRepo;
    }

    public async Task<Response<IEnumerable<GetShippingQueryResponse>>>
        Handle(GetShippingQuery request, CancellationToken cancellationToken)
    {
        var models = await _shippingRepo.GetAll();

        var response = _mapper.Map<IEnumerable<GetShippingQueryResponse>>(models);

        return Success(response);
    }
}

public sealed class GetAllShippingProfile : Profile
{
    public GetAllShippingProfile()
    {
        CreateMap<Shipping, GetShippingQueryResponse>()
            .ForMember(desc => desc.OrderId, options => options.MapFrom(src => src.Id))
            .ForMember(desc => desc.Status, options => options.MapFrom(src => nameof(src.Status)))
            .ForMember(desc => desc.ShippingMethodName, options => options.MapFrom(src => src.ShippingMethod.Name));
    }
}