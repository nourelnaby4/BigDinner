using BigDinner.Application.Features.Notifications.Query;

namespace BigDinner.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NotificationsContoller : ControllerMain
{
    private readonly IMediator _mediator;

    public NotificationsContoller(IMediator mediator)
        => _mediator = mediator;

    [HttpGet("get")]
    public async Task<IActionResult> Get()
        => GetResponse(await _mediator.Send(new GetNotificationQuery()));
}
