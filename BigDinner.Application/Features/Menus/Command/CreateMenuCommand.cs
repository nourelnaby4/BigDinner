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
    private readonly IMenuRepository _menuRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateMenuCommandHandler(
        IMenuRepository menuRepository,
        IUnitOfWork unitOfWork)
    {
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

