using BigDinner.Application.Features.Menus.Command;

namespace BigDinner.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MenuCategoriesController : ControllerMain
{
    private readonly IMediator _mediator;

    public MenuCategoriesController(IMediator mediator)
        => _mediator = mediator;

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateMenuCategoryCommand command)
        => GetResponse(await _mediator.Send(command));
}
