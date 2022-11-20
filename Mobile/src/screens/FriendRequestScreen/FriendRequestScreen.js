import React from 'react';
import GenericListScreen from '../../features/genericList/GenericListScreen/GenericListScreen';
import friendService from '../../services/friendService';
import UserListItem from '../../features/userList/UserListItem/UserListItem';

const FriendRequestScreen = () => (
  <GenericListScreen
    searchEnabled
    fetchItems={friendService.getFriendRequests}
    renderItem={(user) => <UserListItem key={user.id} user={user} />}
    noItemsMessages={['You have no friend requests']}
    emptySearch
  />
);
export default FriendRequestScreen;
