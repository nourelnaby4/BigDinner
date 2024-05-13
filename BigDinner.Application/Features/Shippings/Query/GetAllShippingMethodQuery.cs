using BigDinner.Domain.Models.Shippings;

namespace BigDinner.Application.Features.Shippings.Query;

public record GetAllShippingMethodQuery()  : 
    IRequest<Response<IEnumerable<GetAllShippingMethodQueryResponse>>>;

public record GetAllShippingMethodQueryResponse(Guid id, string name, string description);


public sealed class GetAllShippingMethodQueryHandler : ResponseHandler,
    IRequestHandler<GetAllShippingMethodQuery, Response<IEnumerable<GetAllShippingMethodQueryResponse>>>
{
    private readonly IMapper _mapper;

    private readonly IShippingMethodRepository _shippingMethodRepo;

    public GetAllShippingMethodQueryHandler(IMapper mapper, IShippingMethodRepository shippingMethodRepo)
    {
        _mapper = mapper;
        _shippingMethodRepo = shippingMethodRepo;
    }

    public async Task<Response<IEnumerable<GetAllShippingMethodQueryResponse>>>
        Handle(GetAllShippingMethodQuery request, CancellationToken cancellationToken)
    {
        var models = await _shippingMethodRepo.GetAll();

        var response = _mapper.Map<IEnumerable<GetAllShippingMethodQueryResponse>>(models);

        return Success(response);
    }
}

public sealed class GetAllShippingMethodsProfile : Profile
{
    public GetAllShippingMethodsProfile()
    {
        CreateMap<ShippingMethod, GetAllShippingMethodQueryResponse>();
    }
}