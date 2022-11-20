using Application.Common;
using Application.Repositories;
using Domain.Common;
using Domain.Database;
using Domain.Exceptions;
using Domain.Responses.Users;
using Microsoft.Extensions.Options;

namespace Application.Users.Update;

public sealed class UpdateUserCommandHandler : BaseUpdateHandler<UpdateUserCommand, User, UserResponse>
{
    private readonly IUserRepository userRepository;
    private readonly IOptions<UserSettings> config;

    public UpdateUserCommandHandler(IUserRepository userRepository, IOptions<UserSettings> config) : base(userRepository)
    {
        this.userRepository = userRepository;
        this.config = config;
    }

    public override async Task PreValidation(UpdateUserCommand request)
    {
        request.Config = config.Value;
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

    public override async Task<UserResponse> BuildResponse(UpdateUserCommand request, User databaseEntity)
    {
        return new UserResponse
        {
            Id = databaseEntity.Id,
            Username = databaseEntity.Username,
        };
    }
}