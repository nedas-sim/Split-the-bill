using Domain.Database;

namespace Domain.Responses.Users;

public sealed class UserResponse
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public bool IsFriend { get; set; }

    public UserResponse()
    {

    }

    public UserResponse(User user)
    {
        Id = user.Id;
        Username = user.Username;
    }
}