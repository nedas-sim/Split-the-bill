using Application.Common;
using Application.Repositories;
using Domain.Common;
using Domain.Exceptions;
using Domain.Responses.Finances;

namespace Application.Finances.GetGroupEntries;

public sealed class GetGroupEntriesQueryHandler
    : BaseListHandler<GetGroupEntriesQuery, EntryResponse>
{
    private readonly IEntryRepository entryRepository;
    private readonly IGroupRepository groupRepository;

    public GetGroupEntriesQueryHandler
        (IEntryRepository entryRepository,
         IGroupRepository groupRepository)
    {
        this.entryRepository = entryRepository;
        this.groupRepository = groupRepository;
    }

    public override async Task DatabaseValidation(GetGroupEntriesQuery request, CancellationToken cancellationToken)
    {
        bool isCallerAMember = await groupRepository.IsUserAMember(request.UserId, request.GroupId, cancellationToken);
        if (isCallerAMember is false) 
        {
            throw new ValidationErrorException(ErrorMessages.Group.NotAMember);
        }
    }

    public override async Task<List<EntryResponse>> GetResponses(GetGroupEntriesQuery request, CancellationToken cancellationToken)
        => await entryRepository.GetGroupEntries(request, cancellationToken);

    public override async Task<int> GetTotalCount(GetGroupEntriesQuery request, CancellationToken cancellationToken)
        => await entryRepository.GetGroupEntryCount(request, cancellationToken);
}