using AuthAPI.Data;
using AuthAPI.Services.Models;
using AuthAPI.Utilities;
using System.Security.Claims;

namespace AuthAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public AuthService(IUserRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public ServiceResult<AuthenticatedUserModel> Login(IAuthenticableUser loginModel)
        {
            var user = _userRepository.GetUserByUsername(loginModel.Username);
            if (user == null)
                return ServiceResult<AuthenticatedUserModel>.CreateFailure("Invalid username.");

            var passwordIsValid = PasswordUtility.VerifyPassword(loginModel.Password, user.PasswordHash, user.Salt);
            if (!passwordIsValid)
                return ServiceResult<AuthenticatedUserModel>.CreateFailure("Invalid password.");

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var token = _tokenService.GenerateToken(claims);

            var authUser = new AuthenticatedUserModel { Username = user.Username, Token = token };
            return ServiceResult<AuthenticatedUserModel>.CreateSuccess(authUser);
        }

        public ServiceResult<UserModel> ValidateToken(string token)
        {
            var result = _tokenService.ValidateToken(token);
            if (!result.Success)
                return ServiceResult<UserModel>.CreateFailure(result.Message);

            var claims = result.Data;
            var userId = Int32.Parse(claims.FindFirst(ClaimTypes.NameIdentifier).Value);
            var userName = claims.FindFirst(ClaimTypes.Name).Value;
            var email = claims.FindFirst(ClaimTypes.Email).Value;

            var user = new UserModel(userId, userName, email);

            return ServiceResult<UserModel>.CreateSuccess(user);
        }
    }
}
