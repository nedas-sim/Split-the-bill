using Application.Common;
using Application.Repositories;
using Domain.Database;
using Domain.Exceptions;

namespace Application.Users.Update;

public sealed class UpdateUserCommandHandler : BaseUpdateHandler<UpdateUserCommand, User>
{
    private readonly IUserRepository userRepository;

    public UpdateUserCommandHandler(IUserRepository userRepository) : base(userRepository)
    {
        this.userRepository = userRepository;
    }

    public override async Task DatabaseValidation(UpdateUserCommand request, User databaseEntity, CancellationToken cancellationToken)
    {
        if (databaseEntity.Email != request.Email)
        {
            bool emailExists = await userRepository.EmailExists(request.Email!, cancellationToken);
            if (emailExists)
            {
                throw new ValidationErrorException("Profile with this email already exists");
            }
        }

        if (databaseEntity.Username != request.Username)
        {
            bool usernameExists = await userRepository.UsernameExists(request.Username!, cancellationToken);
            if (usernameExists)
            {
                throw new ValidationErrorException("Profile with this username already exists");
            }
        }
    }
}