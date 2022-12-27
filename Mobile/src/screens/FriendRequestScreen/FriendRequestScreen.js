import React from 'react';
import GenericListScreen from '../../features/genericList/GenericListScreen/GenericListScreen';
import friendService from '../../services/friendService';
import FriendListItem from '../../features/userList/FriendListItem/FriendListItem';

const FriendRequestScreen = () => (
  <GenericListScreen
    searchEnabled
    fetchItems={friendService.getFriendRequests}
    renderItem={(user) => <FriendListItem key={user.id} user={user} />}
    noItemsMessages={['You have no friend requests']}
    emptySearch
  />
);
export default FriendRequestScreen;
