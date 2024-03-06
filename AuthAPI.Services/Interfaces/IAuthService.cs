using AuthAPI.Services.Models;
using System.Security.Claims;

namespace AuthAPI.Services
{
    public interface IAuthService
    {
        ServiceResult<AuthenticatedUserModel> Login (IAuthenticableUser loginModel);
        ServiceResult<UserModel> ValidateToken (string token);
    }
}
