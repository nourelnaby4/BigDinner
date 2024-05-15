using BigDinner.Application.Features.Shippings.Command;
using BigDinner.Application.Features.Shippings.Query;
using BigDinner.Domain.Models.Shippings;

namespace BigDinner.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ShippingsController : ControllerMain
{
    private readonly IMediator _mediator;

    public ShippingsController(IMediator mediator)
        => _mediator = mediator;

    [HttpPost("chage-status/{shippingId}")]
    public async Task<IActionResult> Create(Guid shippingId,ShippingStatus status)
        => GetResponse(await _mediator.Send(new ChangeShippingStatusCommand(shippingId,status)));

    [HttpGet("get")]
    public async Task<IActionResult> Get()
        => GetResponse(await _mediator.Send(new GetShippingQuery()));
}
