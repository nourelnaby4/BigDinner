using BigDinner.Application.Common.Interfaces.Authentication;

namespace BigDinner.Application.Features.Authentication.Login
{
    public class LoginHandler : IRequestHandler<LoginRequest, AuthResponse>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        public LoginHandler(
            UserManager<ApplicationUser> userManager,
            IJwtTokenGenerator jwtTokenGenerator)
        {
            _userManager = userManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }
        public async Task<AuthResponse> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            var authModel = await _jwtTokenGenerator.CreateAuthModel(request.Email,request.Password);
            return authModel;
        }
    }

}
