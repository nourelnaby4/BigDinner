using BigDinner.Domain.Models.Menus;

namespace BigDinner.Application.Features.Menus.Command;

public record EditMenuCommandRequest( string Name, string Description);

public record EditMenuCommand(Guid MenuId, string Name, string Description):IRequest<Response<string>>;

public sealed class EditMenuCommandHandler : ResponseHandler,
    IRequestHandler<EditMenuCommand, Response<string>>
{
    private readonly IMenuRepository _menuRepository;
    private readonly IUnitOfWork _unitOfWork;

    public EditMenuCommandHandler(
        IMenuRepository menuRepository,
        IUnitOfWork unitOfWork)
    {
        _menuRepository = menuRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<string>> Handle(EditMenuCommand request, CancellationToken cancellationToken)
    {
        var menu = await _menuRepository.GetByIdAsync(request.MenuId);

        if (menu is null)
            return NotFound<string>();

        menu.UpdateMenu(request.Name, request.Description);

        _menuRepository.Update(menu);

        await _unitOfWork.CompleteAsync();

        return Created(string.Empty);
    }
}


