import React from 'react';
import { Text, TouchableOpacity } from 'react-native';
import styles from './styles';
import ScreenNames from '../../../common/screenNames';

const GroupListItem = ({ group, navigation }) => {
  const handleItemPress = () => {
    navigation.navigate(ScreenNames.groupDetailsScreen, {
      groupId: group.groupId,
    });
  };

  return (
    <TouchableOpacity style={styles.card} onPress={handleItemPress}>
      <Text style={styles.groupName}>{group.groupName}</Text>
    </TouchableOpacity>
  );
};

export default GroupListItem;
