using BigDinner.Domain.Models.Menus;
using BigDinner.Persistence.Repository;

namespace BigDinner.Application.Features.Menus.Command;

public record CreateMenuCategoryCommand : IRequest<Response<string>>
{
    public string Name { get; set; }

    public string Description { get; set; }
}


public sealed class CreateMenuCategoryCommandHandler : ResponseHandler,
    IRequestHandler<CreateMenuCategoryCommand, Response<string>>
{
    private readonly IMapper _mapper;

    private readonly IMenuRepository _menuRepository;

    private readonly IUnitOfWork _unitOfWork;

    public CreateMenuCategoryCommandHandler(
        IMapper mapper,
        IMenuRepository menuRepository,
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _menuRepository = menuRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<string>> Handle(CreateMenuCategoryCommand request, CancellationToken cancellationToken)
    {
        var menueModel = _mapper.Map<MenuCategory>(request);
        _menuRepository.Add(menueModel);
        await _unitOfWork.CompleteAsync();
        return Created(string.Empty);
    }
}


public class CreateMenuCategoryCommandProfile : Profile
{
    public CreateMenuCategoryCommandProfile()
    {
        CreateMap<CreateMenuCategoryCommand, MenuCategory>()
            .ConstructUsing(cmd => MenuCategory.Create(Guid.NewGuid(), cmd.Name, cmd.Description));
    }
}