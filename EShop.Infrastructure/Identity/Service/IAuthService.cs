using System.Security.Claims;

namespace EShop.Infrastructure.Identity.Service;

public interface IAuthService
{
    string GenerateAccessToken(IEnumerable<Claim> claims);
    string GenerateRefreshToken();
    ClaimsPrincipal GetPrincipalFromToken(string token);
}
