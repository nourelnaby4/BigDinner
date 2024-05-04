using BigDinner.Domain.Models.Menus;
using BigDinner.Persistence.Repository;

namespace BigDinner.Application.Features.Menus.Command;

public record CreateMenuCommand : IRequest<Response<string>>
{
    public string Name { get; set; }

    public string Description { get; set; }
}


public sealed class CreateMenuCommandHandler : ResponseHandler,
    IRequestHandler<CreateMenuCommand, Response<string>>
{
    private readonly IMapper _mapper;

    private readonly IMenuRepository _menuRepository;

    private readonly IUnitOfWork _unitOfWork;

    public CreateMenuCommandHandler(
        IMapper mapper,
        IMenuRepository menuRepository,
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _menuRepository = menuRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<string>> Handle(CreateMenuCommand request, CancellationToken cancellationToken)
    {
        var menueModel = Menu.Create( request.Name, request.Description);
        _menuRepository.Add(menueModel);
        await _unitOfWork.CompleteAsync();
        return Created(string.Empty);
    }
}
