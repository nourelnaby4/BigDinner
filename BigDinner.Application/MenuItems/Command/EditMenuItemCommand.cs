using BigDinner.Domain.Models.Menus;

namespace BigDinner.Application.MenuItems.Command;

public record EditMenuItemCommandRequest(string Name, string Description, Price Price);

public record EditMenuItemCommand(Guid MenuId, Guid MenuItemId, string Name, string Description, Price Price) : IRequest<Response<string>>;

public sealed class EditMenuItemCommandHandler : ResponseHandler,
    IRequestHandler<EditMenuItemCommand, Response<string>>
{
    private readonly IMenuRepository _menuRepository;
    private readonly IUnitOfWork _unitOfWork;

    public EditMenuItemCommandHandler(
        IMenuRepository menuRepository,
        IUnitOfWork unitOfWork)
    {
        _menuRepository = menuRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<string>> Handle(EditMenuItemCommand request, CancellationToken cancellationToken)
    {
        var menu = await _menuRepository.GetByIdAsync(request.MenuId);

        if (menu is null)
            return NotFound<string>("menu is not found");

        var menuItem = menu.Items.SingleOrDefault(x => x.Id == request.MenuItemId);

        if (menuItem is null)
            return NotFound<string>("menu item is not found");

        menuItem.UpdateInfo(request.Name, request.Description, request.Price);

        _menuRepository.Update(menu);

        await _unitOfWork.CompleteAsync();

        return EditSuccess(string.Empty);
    }
}
