using BigDinner.Application.Features.Authentication.Login;

namespace BigDinner.API.Endpoints.Authentications;

public class LoginEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        //app.MapPost("api/auth", async ([FromBody] LoginRequest request, IMediator mediator) =>
        //{
        //    var result = await mediator.Send(request);
        //    return Task.FromResult(result);
        //});

        app.MapGet("api/auth", () =>
        {
            Console.WriteLine("sd");

        });
    }
}

