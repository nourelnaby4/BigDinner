using BigDinner.Domain.Models.Menus;
using BigDinner.Application.Features.Orders.Command;
using BigDinner.Domain.Models.Orders;

namespace BigDinner.Application.Features.Menus.Command;

public record CreateMenuCommand(string Name, string Description, List<MenuItemDto> Items)
    : IRequest<Response<string>>;

public record MenuItemDto(string Name, string Description, Price Price);


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

        foreach(var item in request.Items)
        {
            menueModel.AddMenuItem(MenuItem.Create(item.Name,item.Description,item.Price));
        }

        _menuRepository.Add(menueModel);

        await _unitOfWork.CompleteAsync();

        return Created(string.Empty);
    }
}

