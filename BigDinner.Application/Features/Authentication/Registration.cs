using BigDinner.Application.Common.Abstractions.Authentication;
using BigDinner.Application.Common.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace BigDinner.Application.Features.Authentication;

public record RegistrationRequest( string Username, string Email, string Password) : IRequest<Response<AuthResponse>>;


public class Registration : ResponseHandler,
                            IRequestHandler<RegistrationRequest, Response<AuthResponse>>
{
    public readonly IUserRepo _userManager;
    public readonly IJwtTokenGenerator _jwtTokenGenerator;
    public Registration(
        IUserRepo userManager,
        IJwtTokenGenerator jwtTokenGenerator)
    {
        _userManager = userManager;
        _jwtTokenGenerator = jwtTokenGenerator;
    }


    public async Task<Response<AuthResponse>> Handle(RegistrationRequest request, CancellationToken cancellationToken)
    {
        var userByEmail = await _userManager.GetUserByEmail(request.Email);
        if (userByEmail is not null)
            return BadRequest<AuthResponse>("Email is already exits");

        var userByUsername = await _userManager.GetUserByUsername(request.Username);
        if (userByUsername is not null)
            return BadRequest<AuthResponse>("username is already exits");

        var user = new ApplicationUser()
        {
            Id=Guid.NewGuid().ToString(),
            UserName = request.Username,
            Email = request.Email,
            PasswordHash = HashPassword(request.Password),
        };
        await _userManager.AddAsync(user);
        await _userManager.SaveChangesAsync();
        var authModel = await _jwtTokenGenerator.CreateAuthModel(request.Email, request.Password);
        return Created(authModel);
    }
    private static string HashPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

            // Convert the byte array to hexadecimal string
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < hashedBytes.Length; i++)
            {
                builder.Append(hashedBytes[i].ToString("x2"));
            }

            return builder.ToString();
        }
    }
}

