using System.Collections.Generic;
using System.Security.Claims;

namespace Tcc.Api.Services
{
    public interface ITokenService
    {
        string GenerateAcessToken(IEnumerable<Claim> claims);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
