using System.Security.Claims;

namespace Application.Services;

public interface IAuthorizeService
{
    public bool VerifyPassword(string inputPassword, string hashedPassword);
    public string GenerateHash(string password);
    public string GenerateJWT(Guid id);
    public void ThrowIfJwtIsInvalid(string jwt);
    public IEnumerable<Claim>? ReadToken(string jwt);
}