using BigDinner.Domain.Models.Menus;
using Microsoft.EntityFrameworkCore;

namespace BigDinner.Application.Features.MenuItems.Command;

public record CreateMenuItemCommandRequest(string Name, string Description, Price Price);

public record CreateMenuItemCommand(Guid MenuId, List<CreateMenuItemCommandRequest> items) : IRequest<Response<string>>;

public sealed class CreateMenuItemCommandHandler : ResponseHandler,
    IRequestHandler<CreateMenuItemCommand, Response<string>>
{
    private readonly IMenuRepository _menuRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateMenuItemCommandHandler(
        IMenuRepository menuRepository,
        IUnitOfWork unitOfWork)
    {
        _menuRepository = menuRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<string>> Handle(CreateMenuItemCommand request, CancellationToken cancellationToken)
    {
        var menu = _menuRepository.GetByIdAsync(request.MenuId).Result;

        if (menu is null)
            return NotFound<string>("Menu not found");

        foreach (var item in request.items)
        {
            menu.AddMenuItem(item.Name, item.Description, item.Price);
        }

        _menuRepository.Update(menu);

        await _unitOfWork.CompleteAsync();

        return Created("MenuItems added successfully");
    }
}
