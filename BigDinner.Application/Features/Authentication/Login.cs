using BigDinner.Application.Common.Abstractions.Authentication;
using BigDinner.Application.Common.Models;
using BigDinner.Domain.Models.Base;

namespace BigDinner.Application.Features.Authentication
{
    public record LoginRequest(string Email, string Password) : IRequest<Response<AuthResponse>>;

    public class Login :
        ResponseHandler,
        IRequestHandler<LoginRequest, Response<AuthResponse>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public Login(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IJwtTokenGenerator jwtTokenGenerator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }
        public async Task<Response<AuthResponse>> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
                return Unauthorized<AuthResponse>("email or password is invalid");

            var signIn = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!signIn.Succeeded)
                return Unauthorized<AuthResponse>("email or password is invalid");

            var authModel = await _jwtTokenGenerator.CreateAuthModel(user);
            return Success(authModel);
        }
    }

}
