using Application.Common;
using Application.Repositories;
using Domain.Common;
using Domain.Database;
using Domain.Exceptions;

namespace Application.Finances.CreateEntry;

public sealed class CreateEntryCommandHandler : BaseCreateHandler<CreateEntryCommand, Entry>
{
    private readonly IGroupRepository groupRepository;

    public CreateEntryCommandHandler
        (IEntryRepository entryRepository,
         IGroupRepository groupRepository) 
        : base(entryRepository)
    {
        this.groupRepository = groupRepository;
    }

    public override async Task DatabaseValidation(CreateEntryCommand request, CancellationToken cancellationToken)
    {
        HashSet<Guid> userIds =
            request.FinancialLines
                   .Select(fl => fl.DebtorId)
                   .ToHashSet();

        userIds.Add(request.ActualPayerId);

        bool usersAreMembers = await groupRepository.AreUsersMembers(request.GroupId, userIds.ToList(), cancellationToken);

        if (usersAreMembers is false)
        {
            throw new ValidationErrorException(ErrorMessages.Group.NotAMemberMultiple);
        }
    }
}