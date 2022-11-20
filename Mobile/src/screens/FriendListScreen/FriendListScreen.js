import React from 'react';
import GenericListScreen from '../../features/genericList/GenericListScreen/GenericListScreen';
import friendService from '../../services/friendService';
import UserListItem from '../../features/userList/UserListItem/UserListItem';

const FriendListScreen = () => (
  <GenericListScreen
    searchEnabled
    fetchItems={friendService.getFriends}
    renderItem={(user) => <UserListItem key={user.id} user={user} />}
    noItemsMessages={['No users found']}
    emptySearch
  />
);

export default FriendListScreen;
