import React from 'react';
import userService from '../../services/userService';
import FriendListItem from '../../features/userList/FriendListItem/FriendListItem';
import GenericListScreen from '../../features/genericList/GenericListScreen/GenericListScreen';

const UserListScreen = () => (
  <GenericListScreen
    searchEnabled
    fetchItems={userService.getUsers}
    renderItem={(user) => <FriendListItem key={user.id} user={user} />}
    noItemsMessages={['No users found', 'Type username or email to find someone!']}
  />
);
export default UserListScreen;
