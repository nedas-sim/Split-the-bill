using Application.Common;
using Domain.Database;

namespace Application.Authorization.Registration;

public sealed class RegisterCommand : BaseCreateRequest<User>
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string RepeatPassword { get; set; }

    public override User BuildEntity()
    {
        throw new NotImplementedException();
    }
}