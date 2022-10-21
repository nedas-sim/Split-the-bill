using Application.Repositories;
using Application.Services;
using Domain.Common;
using Domain.Common.Results;
using Domain.Database;
using Domain.Results;
using MediatR;
using Microsoft.Extensions.Options;

namespace Application.Authorization.Registration;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, BaseResult<Unit>>
{
    private readonly IUserRepository userRepository;
    private readonly IAuthorizeService authorizeService;
    private readonly UserSettings config;

    public RegisterCommandHandler(IUserRepository userRepository,
                                  IAuthorizeService authorizeService,
                                  IOptions<UserSettings> config)
    {
        this.userRepository = userRepository;
        this.authorizeService = authorizeService;
        this.config = config.Value;
    }

    public async Task<BaseResult<Unit>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        request.SetConfigurations(config);

        if (request.IsValid(out string? errorMessage) is false)
        {
            return new ValidationErrorResult<Unit>
            {
                Message = errorMessage!,
            };
        }

        bool emailExists = await userRepository.EmailExists(request.Email, cancellationToken);
        if (emailExists)
        {
            return new ValidationErrorResult<Unit>
            {
                Message = "User with this email already exists",
            };
        }

        User user = request.BuildEntity();
        user.Password = authorizeService.GenerateHash(request.Password);
        await userRepository.Update(user, cancellationToken);
        return new NoContentResult<Unit>();
    }
}