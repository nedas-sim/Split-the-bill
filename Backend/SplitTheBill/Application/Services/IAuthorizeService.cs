namespace Application.Services;

public interface IAuthorizeService
{
    public bool VerifyPassword(string inputPassword, string hashedPassword);
    public string GenerateHash(string password);
    public string GenerateJWT(Guid id);
}