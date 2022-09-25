namespace Domain.Common;

public sealed class ApiConfiguration
{
    public ConnectionStrings ConnectionStrings { get; set; }
}

public sealed record ConnectionStrings(string DefaultConnection);