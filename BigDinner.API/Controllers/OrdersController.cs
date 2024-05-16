using BigDinner.Application.Features.Orders.Command;
using BigDinner.Application.Features.Orders.Query;

namespace BigDinner.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerMain
{
    private readonly IMediator _mediator;

    public OrdersController(IMediator mediator)
        => _mediator = mediator;

    [HttpGet("get")]
    public async Task<IActionResult> Get()
        => GetResponse(await _mediator.Send(new GetOrderQuery()));

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateOrderCommand command)
        => GetResponse(await _mediator.Send(command));
}
