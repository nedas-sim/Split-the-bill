namespace Domain.Common;

/*public sealed class ApiConfiguration
{
    public ConnectionStrings ConnectionStrings { get; set; }
    public Users Users { get; set; }
    public JwtConfig JwtConfig { get; set; }
}*/

//public sealed record ConnectionStrings(string DefaultConnection);
//public sealed record UserSettings(int MinPasswordLength);
//public sealed record JwtConfig(string SecretKey, int ExpirationMinutes);

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