using Application.Common;
using Domain.Common;
using Domain.Extensions;
using Domain.Responses.Users;

namespace Application.Users.GetUserList;

public sealed class GetUserListQuery : PagingParameters, IListRequest<UserResponse>
{
    internal Guid CallingUserId { get; set; }
    public void SetCallingUserId(Guid id) => CallingUserId = id;

    public string Search { get; set; }

    internal UserSettings Config;

    private GetUserListQueryValidator? _validator = null;

    public bool IsValid(out string? errorMessage)
    {
        _validator ??= new()
        {
            Search = Search,
            Config = Config,
        };

        return _validator.IsValid(out errorMessage);
    }

    internal class GetUserListQueryValidator : BaseValidation
    {
        public override string ApiErrorMessagePrefix => ErrorMessages.User.GetListRequestPrefix;

        internal string Search { get; set; }
        internal UserSettings Config { get; set; }

        public override IEnumerable<(bool Success, string ErrorMessage)> ValidateProperties()
        {
            bool validUsernameLength = Search?.Length >= Config.MinUsernameLength;

            yield return (validUsernameLength, ErrorMessages.User.MinimumSearchLength(Config.MinUsernameLength));
        }
    }
}