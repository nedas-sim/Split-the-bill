import React from 'react';
import ScreenNames from '../../../common/screenNames';
import CoreGroupListItem from '../../../components/CoreGroupListItem/CoreGroupListItem';

const GroupListItem = ({ group, navigation }) => {
  const handleItemPress = () => {
    navigation.navigate(ScreenNames.groupDetailsScreen, {
      groupId: group.groupId,
      groupName: group.groupName,
    });
  };

  return <CoreGroupListItem group={group} onCardPress={handleItemPress} />;
};

export default GroupListItem;
