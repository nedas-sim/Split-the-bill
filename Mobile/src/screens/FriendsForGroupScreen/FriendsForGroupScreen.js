import React from 'react';
import GenericListScreen from '../../features/genericList/GenericListScreen/GenericListScreen';
import groupService from '../../services/groupService';
import FriendForGroupItem from '../../features/user/FriendForGroupItem/FriendForGroupItem';
import { GroupInvitationContext } from '../../common/context';

const FriendsForGroupScreen = ({ route }) => {
  const { groupId } = route.params;
  return (
    <GroupInvitationContext.Provider value={{ groupId }}>
      <GenericListScreen
        emptySearch
        searchEnabled
        fetchItems={groupService.getFriendSuggestions}
        queryParams={{ groupId }}
        noItemsMessages={["There aren't any friends that you can invite to this group"]}
        renderItem={(user) => <FriendForGroupItem key={user.id} user={user} groupId={groupId} />}
      />
    </GroupInvitationContext.Provider>
  );
};

export default FriendsForGroupScreen;
