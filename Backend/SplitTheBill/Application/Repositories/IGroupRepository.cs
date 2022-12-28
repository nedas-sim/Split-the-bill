using Application.Groups.GetGroupsForFriend;
using Application.Groups.GetInvitations;
using Application.Groups.GetUsersGroupList;
using Application.Groups.SendInvitation;
using Domain.Common;
using Domain.Database;
using Domain.Responses.Groups;

namespace Application.Repositories;

public interface IGroupRepository : IBaseRepository<Group>
{
    public Task<List<GroupResponse>> GetUsersGroups(IPaging pagingParameters, GetUsersGroupListQuery query, CancellationToken cancellationToken = default);
    public Task<int> UserGroupsCount(GetUsersGroupListQuery query, CancellationToken cancellationToken = default);
    public Task<bool> IsUserAMember(Guid userId, Guid groupId, CancellationToken cancellationToken = default);
    public Task<UserGroup?> GetUserMembership(Guid userId, Guid groupId, CancellationToken cancellationToken = default);
    public Task SendGroupInvitation(SendInvitationCommand request, CancellationToken cancellationToken = default);
    public Task AcceptInvitation(UserGroup userGroup, CancellationToken cancellationToken = default);
    public Task RejectInvitation(UserGroup userGroup, CancellationToken cancellationToken = default);

    public Task<List<GroupResponse>> GetPotentialGroupsForFriend(GetGroupsForFriendQuery request, CancellationToken cancellationToken = default);
    public Task<int> GetPotentialGroupsForFriendCount(GetGroupsForFriendQuery request, CancellationToken cancellationToken = default);

    public Task<List<GroupResponse>> GetInvitations(GetInvitationsQuery request, CancellationToken cancellationToken = default);
    public Task<int> GetInvitationCount(GetInvitationsQuery request, CancellationToken cancellationToken = default);
}