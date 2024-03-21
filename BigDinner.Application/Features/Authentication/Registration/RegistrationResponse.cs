namespace BigDinner.Application.Features.Authentication.Registration
{
    public record RegistrationResponse(Guid Id, string FirstName, string LastName, string Email, string Token);


}
