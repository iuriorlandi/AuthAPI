using System.Security.Claims;

namespace AuthAPI.Services
{
    public interface ITokenService
    {
        string GenerateToken(IEnumerable<Claim> claims);
        ServiceResult<ClaimsPrincipal> ValidateToken(string token);
    }
}
