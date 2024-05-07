using BigDinner.Application.Features.Customers.Command;
using BigDinner.Application.Features.Customers.Query;

namespace BigDinner.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomersController : ControllerMain
{
    private readonly IMediator _mediator;

    public CustomersController(IMediator mediator)
        => _mediator = mediator;

    [HttpGet("get")]
    public async Task<IActionResult> Get()
        => GetResponse(await _mediator.Send(new GetAllCustomerQuery()));

    [HttpGet("getById/{id}")]
    public async Task<IActionResult> Get(Guid id)
        => GetResponse(await _mediator.Send(new GetByIdCustomerQuery(id)));

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateCustomerCommand command)
        => GetResponse(await _mediator.Send(command));
}
