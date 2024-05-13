using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BigDinner.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ShippingMethodsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ShippingMethodsController(IMediator mediator)
        => _mediator = mediator;


    //[HttpPost("create")]
    //public async Task<IActionResult> Create([FromBody] CreateCustomerCommand command)
    //    => GetResponse(await _mediator.Send(command));
}
