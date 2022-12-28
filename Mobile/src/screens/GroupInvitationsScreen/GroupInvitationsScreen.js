import React from 'react';
import GenericListScreen from '../../features/genericList/GenericListScreen/GenericListScreen';
import groupService from '../../services/groupService';
import GroupInvitationItem from '../../features/group/GroupInvitationItem/GroupInvitationItem';

const GroupInvitationsScreen = () => (
  <GenericListScreen
    searchEnabled
    emptySearch
    fetchItems={groupService.getInvitations}
    noItemsMessages={['There are no invitations']}
    renderItem={(group) => <GroupInvitationItem key={group.groupId} group={group} />}
  />
);

export default GroupInvitationsScreen;
