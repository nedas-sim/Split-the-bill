using Application.Services;
using Domain.Common;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Services;

public class AuthorizeService : IAuthorizeService
{
    private const int SaltLength = 16;
    private const int HashLength = 20;

    private readonly JwtConfig config;

    public AuthorizeService(IOptions<JwtConfig> config)
    {
        this.config = config.Value;
    }

    public string GenerateHash(string password)
    {
        byte[] salt;
        new RNGCryptoServiceProvider().GetBytes(salt = new byte[SaltLength]);

        var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
        byte[] hash = pbkdf2.GetBytes(HashLength);

        byte[] hashBytes = new byte[SaltLength + HashLength];
        Array.Copy(salt, 0, hashBytes, 0, SaltLength);
        Array.Copy(hash, 0, hashBytes, SaltLength, HashLength);

        return Convert.ToBase64String(hashBytes);
    }

    public string GenerateJWT(Guid id)
    {
        JwtSecurityTokenHandler tokenHandler = new();
        byte[] key = Encoding.ASCII.GetBytes(config.SecretKey);

        SecurityTokenDescriptor tokenDescriptor = new()
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new(ClaimTypes.NameIdentifier, id.ToString()),
            }),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public bool VerifyPassword(string inputPassword, string hashedPassword)
    {
        byte[] hashBytes = Convert.FromBase64String(hashedPassword);
        byte[] salt = new byte[SaltLength];
        Array.Copy(hashBytes, 0, salt, 0, SaltLength);
        Rfc2898DeriveBytes pbkdf2 = new(inputPassword, salt, 100000);
        byte[] hash = pbkdf2.GetBytes(HashLength);

        bool hashesMatch = true;
        for (int i = 0; i < HashLength; i++)
        {
            if (hashBytes[i + SaltLength] != hash[i])
            {
                hashesMatch = false;
            }
        }

        return hashesMatch;
    }

    public void ThrowIfJwtIsInvalid(string jwt)
    {
        JwtSecurityTokenHandler tokenHandler = new();
        TokenValidationParameters validationParameters = GetValidationParameters();
        tokenHandler.ValidateToken(jwt, validationParameters, out _);
    }

    public IEnumerable<Claim>? ReadToken(string jwt)
    {
        JwtSecurityTokenHandler handler = new();
        SecurityToken jsonToken = handler.ReadToken(jwt);

        if (jsonToken is not JwtSecurityToken token)
        {
            return null;
        }

        return token.Claims;
    }

    private TokenValidationParameters GetValidationParameters()
    {
        return new TokenValidationParameters()
        {
            ValidateLifetime = false,
            ValidateAudience = false,
            ValidateIssuer = false,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.SecretKey)),
        };
    }
}