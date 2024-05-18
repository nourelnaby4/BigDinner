using BigDinner.Application.Common.Abstractions.Authentication;
using BigDinner.Application.Common.Models;
using BigDinner.Domain.Models.Base;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace BigDinner.Application.Features.Authentication;

public record RegistrationRequest( string Username, string Email, string Password) : IRequest<Response<AuthResponse>>;


public class Registration : ResponseHandler,
                            IRequestHandler<RegistrationRequest, Response<AuthResponse>>
{
    public readonly UserManager<ApplicationUser> _userManager;
    public readonly IJwtTokenGenerator _jwtTokenGenerator;
    public Registration(
        UserManager<ApplicationUser> userManager,
        IJwtTokenGenerator jwtTokenGenerator)
    {
        _userManager = userManager;
        _jwtTokenGenerator = jwtTokenGenerator;
    }


    public async Task<Response<AuthResponse>> Handle(RegistrationRequest request, CancellationToken cancellationToken)
    {
        var userByEmail = await _userManager.FindByEmailAsync(request.Email);
        if (userByEmail is not null)
            return BadRequest<AuthResponse>("Email is already exits");

        var userByUsername = await _userManager.FindByEmailAsync(request.Username);
        if (userByUsername is not null)
            return BadRequest<AuthResponse>("username is already exits");

        var user = new ApplicationUser()
        {
            Id=Guid.NewGuid().ToString(),
            UserName = request.Username,
            Email = request.Email,
        };
        await _userManager.CreateAsync(user,request.Password);

        var authModel = await _jwtTokenGenerator.CreateAuthModel(user);
        return Created(authModel);
    }
}

