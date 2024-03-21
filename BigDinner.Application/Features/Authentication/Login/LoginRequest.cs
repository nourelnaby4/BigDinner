namespace BigDinner.Application.Features.Authentication.Login
{
    public record LoginRequest(string Email, string Password) : IRequest<LoginResponse>;

}
