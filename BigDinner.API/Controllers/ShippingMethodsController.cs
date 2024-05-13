using BigDinner.Application.Features.Shippings.Command;
using BigDinner.Application.Features.Shippings.Query;

namespace BigDinner.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ShippingMethodsController : ControllerMain
{
    private readonly IMediator _mediator;

    public ShippingMethodsController(IMediator mediator)
        => _mediator = mediator;

    [HttpGet("get")]
    public async Task<IActionResult> Get()
        => GetResponse(await _mediator.Send(new GetAllShippingMethodQuery()));

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateShippingMethodCommand command)
        => GetResponse(await _mediator.Send(command));
}