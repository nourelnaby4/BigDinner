using BigDinner.Application.Features.Menus.Command;
using BigDinner.Application.Features.Menus.Query;

namespace BigDinner.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MenusController : ControllerMain
{
    private readonly IMediator _mediator;

    public MenusController(IMediator mediator)
        => _mediator = mediator;

    [HttpGet("get")]
    public async Task<IActionResult> Get()
        => GetResponse(await _mediator.Send(new GetAllMenuQuery()));

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateMenuCommand command)
        => GetResponse(await _mediator.Send(command));

    [HttpPut("edit/{id}")]
    public async Task<IActionResult> Edit(Guid id, [FromBody] EditMenuCommandRequest request)
        => GetResponse(await _mediator.Send(new EditMenuCommand(id,request.Name,request.Description)));
}
