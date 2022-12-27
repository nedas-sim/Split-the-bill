import React from 'react';
import GenericListScreen from '../../features/genericList/GenericListScreen/GenericListScreen';
import friendService from '../../services/friendService';
import FriendListItem from '../../features/user/FriendListItem/FriendListItem';

const FriendListScreen = () => (
  <GenericListScreen
    searchEnabled
    fetchItems={friendService.getFriends}
    renderItem={(user) => <FriendListItem key={user.id} user={user} />}
    noItemsMessages={['No users found']}
    emptySearch
  />
);

export default FriendListScreen;
