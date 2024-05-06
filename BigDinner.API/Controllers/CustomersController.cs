using BigDinner.Application.Features.Customers.Command;

namespace BigDinner.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomersController : ControllerMain
{
    private readonly IMediator _mediator;

    public CustomersController(IMediator mediator)
        => _mediator = mediator;

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateCustomerCommand command)
        => GetResponse(await _mediator.Send(command));
}
