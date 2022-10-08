namespace Domain.Common;

public sealed class ConnectionStrings
{
    public string DefaultConnection { get; set; }
}

public sealed class JwtConfig
{
    public string SecretKey { get; set; }
    public int ExpirationMinutes { get; set; }
}

public sealed class UserSettings
{
    public int MinPasswordLength { get; set; }
}