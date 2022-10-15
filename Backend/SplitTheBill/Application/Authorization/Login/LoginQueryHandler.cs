using Application.Repositories;
using Application.Services;
using Domain.Common.Results;
using Domain.Database;
using Domain.Responses.Authorization;
using Domain.Results;
using MediatR;

namespace Application.Authorization.Login;

public sealed class LoginQueryHandler : IRequestHandler<LoginQuery, BaseResult<LoginResponse>>
{
    private readonly IAuthorizeService authorizeService;
    private readonly IUserRepository userRepository;

    public LoginQueryHandler(IAuthorizeService authorizeService,
                             IUserRepository userRepository)
    {
        this.authorizeService = authorizeService;
        this.userRepository = userRepository;
    }

    public async Task<BaseResult<LoginResponse>> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        User? user = await userRepository.GetByEmail(request.Email);
        if (user is null)
        {
            return BuildIncorrectRequestResult();
        }

        bool passwordCorrect = authorizeService.VerifyPassword(request.Password, user.Password);
        if (passwordCorrect is false)
        {
            return BuildIncorrectRequestResult();
        }

        string jwt = authorizeService.GenerateJWT(user.Id);
        return new LoginResponse
        {
            Jwt = jwt,
        };
    }

    private static BaseResult<LoginResponse> BuildIncorrectRequestResult()
    {
        return new NotFoundResult<LoginResponse>
        {
            Message = "Email or password is incorrect",
        };
    }
}