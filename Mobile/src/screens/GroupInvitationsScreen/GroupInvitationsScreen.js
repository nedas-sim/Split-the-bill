import React from 'react';
import GenericListScreen from '../../features/genericList/GenericListScreen/GenericListScreen';
import groupService from '../../services/groupService';
import GroupListItem from '../../features/group/GroupListItem/GroupListItem';

const GroupInvitationsScreen = ({ navigation }) => (
  <GenericListScreen
    searchEnabled
    emptySearch
    fetchItems={groupService.getInvitations}
    noItemsMessages={['There are no invitations']}
    renderItem={(group) => (
      <GroupListItem key={group.groupId} group={group} navigation={navigation} />
    )}
  />
);

export default GroupInvitationsScreen;
