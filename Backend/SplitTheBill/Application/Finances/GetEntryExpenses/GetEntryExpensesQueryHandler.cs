using Application.Common;
using Application.Repositories;
using Domain.Common;
using Domain.Exceptions;
using Domain.Responses.Finances;

namespace Application.Finances.GetEntryExpenses;

public sealed class GetEntryExpensesQueryHandler
    : BaseListHandler<GetEntryExpensesQuery, EntryExpenseResponse>
{
    private readonly IEntryRepository entryRepository;
    private readonly IGroupRepository groupRepository;

    public GetEntryExpensesQueryHandler
        (IEntryRepository entryRepository,
         IGroupRepository groupRepository)
    {
        this.entryRepository = entryRepository;
        this.groupRepository = groupRepository;
    }

    public override async Task DatabaseValidation(GetEntryExpensesQuery request, CancellationToken cancellationToken)
    {
        List<Guid> userIds = new() { request.UserId };
        if (request.FriendId is not null)
        {
            userIds.Add(request.FriendId.Value);
        }

        bool usersAreMembers = await groupRepository.AreUsersMembers(request.GroupId, userIds, cancellationToken);
        if (usersAreMembers is false)
        {
            throw new ValidationErrorException(ErrorMessages.Group.NotAMemberMultiple);
        }
    }

    public override async Task<List<EntryExpenseResponse>> GetResponses(GetEntryExpensesQuery request, CancellationToken cancellationToken)
        => await entryRepository.GetEntryExpenses(request, cancellationToken);

    public override async Task<int> GetTotalCount(GetEntryExpensesQuery request, CancellationToken cancellationToken)
        => await entryRepository.GetEntryExpenseCount(request, cancellationToken);
}