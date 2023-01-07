using Application.Common;
using Domain.Responses.Finances;

namespace Application.Finances.GetGroupEntries;

public sealed class GetGroupEntriesQuery : BaseListRequest<EntryResponse>
{
    #region Auth ID
    internal Guid UserId { get; set; }
    public void SetUserId(Guid userId) => UserId = userId;
    #endregion
    #region API Params
    public string? Search { get; set; }
    public Guid GroupId { get; set; }
    #endregion
}