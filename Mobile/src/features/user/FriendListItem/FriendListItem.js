import React from 'react';
import CoreUserListItem from '../../../components/CoreUserListItem/CoreUserListItem';
import ScreenNames from '../../../common/screenNames';

const FriendListItem = ({ user, navigation }) => {
  const handleCardPress = () => {
    navigation.navigate(ScreenNames.friendDetailsScreen, {
      userId: user.id,
      userName: user.username,
    });
  };
  return <CoreUserListItem user={user} onCardPress={handleCardPress} />;
};

export default FriendListItem;
