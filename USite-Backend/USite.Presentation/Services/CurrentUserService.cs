using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using USite.Application.Common.Interfaces;

namespace USite.Presentation.Services;
public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? UserId => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier) ?? GetSubFromAccessToken(_httpContextAccessor.HttpContext?.Request.Query["access_token"].ToString());

    public string GetSubFromAccessToken(string? accessToken)
    {
        if (string.IsNullOrEmpty(accessToken)) return "";

        var jwtToken = new JwtSecurityTokenHandler().ReadToken(accessToken) as JwtSecurityToken;

        if (jwtToken == null)
            throw new ArgumentException("Invalid access token");

        var subClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "sub");

        if (subClaim == null)
            throw new ArgumentException("Access token does not contain a 'sub' claim");

        return subClaim.Value;
    }
}