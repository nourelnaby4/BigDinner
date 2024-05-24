using BigDinner.API.Base;
using BigDinner.Application.Features.MenuItems.Command;

namespace BigDinner.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MenuItemsController : ControllerMain
{
    private readonly IMediator _mediator;

    public MenuItemsController(IMediator mediator)
        => _mediator = mediator;

    [HttpPost("create")]
    public async Task<IActionResult> Create( [FromBody] CreateMenuItemCommand request)
        => GetResponse(await _mediator.Send(request));

    [HttpPut("edit/{meneId}/{menuItemId}")]
    public async Task<IActionResult> Edit(Guid meneId, Guid menuItemId, [FromBody] EditMenuItemCommandRequest request)
        => GetResponse(await _mediator.Send(new EditMenuItemCommand(meneId,menuItemId, request.Name, request.Description,request.Price)));
}
