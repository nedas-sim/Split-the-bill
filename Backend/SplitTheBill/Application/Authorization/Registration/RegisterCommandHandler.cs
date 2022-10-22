using Application.Common;
using Application.Repositories;
using Application.Services;
using Domain.Common;
using Domain.Database;
using Domain.Exceptions;
using Microsoft.Extensions.Options;

namespace Application.Authorization.Registration;

public class RegisterCommandHandler : BaseCreateHandler<RegisterCommand, User>
{
    private readonly IUserRepository userRepository;
    private readonly IAuthorizeService authorizeService;
    private readonly UserSettings config;

    public RegisterCommandHandler(IUserRepository userRepository,
                                  IAuthorizeService authorizeService,
                                  IOptions<UserSettings> config) : base(userRepository)
    {
        this.userRepository = userRepository;
        this.authorizeService = authorizeService;
        this.config = config.Value;
    }

    public override async Task PreValidation(RegisterCommand request)
    {
        request.SetConfigurations(config);
    }

    public override async Task DatabaseValidation(RegisterCommand request, CancellationToken cancellationToken)
    {
        bool emailExists = await userRepository.EmailExists(request.Email, cancellationToken);
        if (emailExists)
        {
            throw new ValidationErrorException("User with this email already exists");
        }
    }

    public override async Task PostEntityBuilding(RegisterCommand request, User entity, CancellationToken cancellationToken)
    {
        entity.Password = authorizeService.GenerateHash(request.Password);
    }
}