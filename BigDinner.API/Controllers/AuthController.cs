using BigDinner.Application.Features.Authentication;

namespace BigDinner.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerMain
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator) => _mediator = mediator;

        [HttpPost("sing-up")]
        public async Task<IActionResult> SignUp([FromBody] RegistrationRequest request)
              => GetResponse(await _mediator.Send(request));


        [HttpPost("sing-in")]
        public async Task<IActionResult> SignIn([FromBody] LoginRequest request)
              => GetResponse(await _mediator.Send(request));
    }
}
