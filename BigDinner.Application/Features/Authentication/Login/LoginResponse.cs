namespace BigDinner.Application.Features.Authentication.Login
{
    public record LoginResponse(Guid Id, string FirstName, string LastName, string Email, string Token);

}
