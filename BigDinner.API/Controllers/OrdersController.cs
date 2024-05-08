using BigDinner.Application.Features.Orders.Command;

namespace BigDinner.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerMain
{
    private readonly IMediator _mediator;

    public OrdersController(IMediator mediator)
        => _mediator = mediator;

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateOrderCommand command)
        => GetResponse(await _mediator.Send(command));
}
