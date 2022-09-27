using Application.Repositories;
using Domain.Common.Results;
using Domain.Database;
using Domain.Responses.Users;
using Domain.Results;
using MediatR;

namespace Application.Users.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, BaseResult<UserResponse>>
{
    private readonly IUserRepository userRepository;

    public CreateUserCommandHandler(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task<BaseResult<UserResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        string? validationError = request.GetValidationError();

        if (validationError is not null)
        {
            return new ValidationErrorResult<UserResponse>
            {
                Message = validationError,
            };
        }

        User user = request.Create();
        user = await userRepository.Create(user, cancellationToken);
        return new UserResponse(user);
    }
}