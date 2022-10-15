using Application.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Encodings.Web;
using WebApi.Controllers;

namespace WebApi.Middlewares;

public class CustomAuthenticationSchemeOptions
        : AuthenticationSchemeOptions
{
}

public class AuthorizationMiddleware : AuthenticationHandler<CustomAuthenticationSchemeOptions>
{
    private readonly IAuthorizeService authorizeService;

    public AuthorizationMiddleware(IOptionsMonitor<CustomAuthenticationSchemeOptions> options, 
                                   ILoggerFactory logger, 
                                   UrlEncoder encoder, 
                                   ISystemClock clock,
                                   IAuthorizeService authorizeService) 
        : base(options, logger, encoder, clock)
    {
        this.authorizeService = authorizeService;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        string? token = Request.Cookies[BaseController.JwtCookieKey];
        if (token is null)
        {
            return AuthenticateResult.Fail("Token not found");
        }

        try
        {
            authorizeService.ThrowIfJwtIsInvalid(token);
        }
        catch
        {
            return AuthenticateResult.Fail("Invalid token");
        }

        IEnumerable<Claim>? claims = authorizeService.ReadToken(token);
        if (claims is null)
        {
            return AuthenticateResult.Fail("Invalid token");
        }

        ClaimsIdentity claimsIdentity = new(claims, nameof(AuthorizationMiddleware));
        AuthenticationTicket ticket = new(new ClaimsPrincipal(claimsIdentity), Scheme.Name);
        return AuthenticateResult.Success(ticket);
    }
}
