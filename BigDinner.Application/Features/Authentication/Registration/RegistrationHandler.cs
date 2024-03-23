using BigDinner.Application.Common.Interfaces.Authentication;
using Microsoft.AspNetCore.Identity;

namespace BigDinner.Application.Features.Authentication.Registration;

public class RegistrationHandler : IRequestHandler<RegistrationRequest, AuthResponse>
{
    public readonly UserManager<ApplicationUser> _userManager;
    public readonly IJwtTokenGenerator _jwtTokenGenerator;
    public RegistrationHandler(
        UserManager<ApplicationUser> userManager,
        IJwtTokenGenerator jwtTokenGenerator)
    {
        _userManager = userManager;
        _jwtTokenGenerator = jwtTokenGenerator;
    }


    public async Task<AuthResponse> Handle(RegistrationRequest request, CancellationToken cancellationToken)
    {
        var userByEmail = await _userManager.FindByEmailAsync(request.Email);
        if (userByEmail is not null)
            throw new InvalidOperationException("Email is already exits");

        var userByUsername = await _userManager.FindByNameAsync(request.Username);
        if (userByUsername is not null)
            throw new InvalidOperationException("username is already exits");

        var user = new ApplicationUser
        {
            UserName = request.Username,
            Email = request.Email,

        };
        await _userManager.CreateAsync(user, request.Password);
        var authModel = await _jwtTokenGenerator.CreateAuthModel(request.Email, request.Password);
        return authModel;
    }
}

