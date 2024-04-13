using BigDinner.Application.Common.Interfaces.Authentication;
using BigDinner.Application.Common.Models;

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
            IJwtTokenGenerator jwtTokenGenerator)
        {
            _userManager = userManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }
        public async Task<Response<AuthResponse>> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
                throw new UnauthorizedAccessException("email is not exist");

            var signIn = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!signIn.Succeeded)
                throw new UnauthorizedAccessException("password is incorrect");

            var authModel = await _jwtTokenGenerator.CreateAuthModel(request.Email, request.Password);
            return Success(authModel);
        }
    }

}
