import React from 'react';
import GenericListScreen from '../../features/genericList/GenericListScreen/GenericListScreen';
import friendService from '../../services/friendService';
import GroupForFriendItem from '../../features/group/GroupForFriendItem/GroupForFriendItem';
import { GroupInvitationContext } from '../../common/context';

const GroupsForFriendScreen = ({ route }) => {
  const { userId } = route.params;
  return (
    <GroupInvitationContext.Provider value={{ userId }}>
      <GenericListScreen
        emptySearch
        searchEnabled
        fetchItems={friendService.getGroupSuggestions}
        queryParams={{ friendId: userId }}
        noItemsMessages={["There aren't any groups that you can invite your friend to"]}
        renderItem={(group) => <GroupForFriendItem key={group.groupId} group={group} />}
      />
    </GroupInvitationContext.Provider>
  );
};

export default GroupsForFriendScreen;
