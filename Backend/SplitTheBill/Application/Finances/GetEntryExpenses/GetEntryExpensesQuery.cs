using Application.Common;
using Domain.Common;
using Domain.Responses.Finances;

namespace Application.Finances.GetEntryExpenses;

public sealed class GetEntryExpensesQuery : BaseListRequest<EntryExpenseResponse>
{
    #region Auth ID
    internal Guid UserId { get; set; }
    public void SetUserId(Guid userId) => UserId = userId;
    #endregion
    #region API Params
    public Guid GroupId { get; set; }
    public Guid? FriendId { get; set; }
    #endregion
    #region Overrides
    public override string ApiErrorMessagePrefix() => ErrorMessages.Finances.GetExpensesRequestPrefix;

    public override IEnumerable<(bool Success, string ErrorMessage)> ValidateProperties()
    {
        bool sameUserId;
        if (FriendId is null)
        {
            sameUserId = false;
        }
        else
        {
            sameUserId = UserId == FriendId;
        }

        yield return (sameUserId is false, ErrorMessages.Finances.InvalidUserFilter);
    }
    #endregion
}