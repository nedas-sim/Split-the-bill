using Domain.Database;

namespace Domain.Responses.Users;

public sealed class UserResponse
{
    public Guid Id { get; set; }
    public string Username { get; set; }

    public UserResponse(User user)
    {
        Id = user.Id;
        Username = user.Username;
    }
}