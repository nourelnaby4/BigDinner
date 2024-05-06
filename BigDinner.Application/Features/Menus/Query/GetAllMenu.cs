using BigDinner.Application.Features.Menus.Command;
using BigDinner.Domain.Models.Menus;
using BigDinner.Application.Common.Abstractions.Repository;

namespace BigDinner.Application.Features.Menus.Query;

public sealed record GetAllMenuQuery() 
    : IRequest<Response<IEnumerable<GetAllMenuQueryResponse>>>;


public sealed record GetAllMenuQueryResponse(Guid Id,string Name ,string Description);


public sealed class GetAllMenuQueryHandler : ResponseHandler,
    IRequestHandler<GetAllMenuQuery, Response<IEnumerable<GetAllMenuQueryResponse>>>
{
    private readonly IMapper _mapper;

    private readonly IMenuRepository _menuRepository;

    private readonly IUnitOfWork _unitOfWork;

    public GetAllMenuQueryHandler(
        IMapper mapper,
        IMenuRepository menuRepository,
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _menuRepository = menuRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<IEnumerable<GetAllMenuQueryResponse>>>
        Handle(GetAllMenuQuery request, CancellationToken cancellationToken)
    {

        var model = await _menuRepository.GetAll();
        var response = _mapper.Map<IEnumerable< GetAllMenuQueryResponse>>(model);

        return Success<IEnumerable<GetAllMenuQueryResponse>>(response);
    }
}

public class GetAllMenuQueryProfile : Profile
{
    public GetAllMenuQueryProfile()
    {
        CreateMap<Menu, GetAllMenuQueryResponse>();
    }
}
