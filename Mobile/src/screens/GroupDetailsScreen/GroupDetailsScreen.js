import React, { useCallback } from 'react';
import { Text, View, BackHandler, Button } from 'react-native';
import { useFocusEffect } from '@react-navigation/native';
import ScreenNames from '../../common/screenNames';
import backHandlerHelper from '../../common/backHandlerHelper';

const GroupDetailsScreen = ({ route, navigation }) => {
  const { groupId, groupName } = route.params;

  useFocusEffect(
    useCallback(() => {
      // setup event listener on mount
      backHandlerHelper.setBackScreen(
        BackHandler,
        navigation,
        ScreenNames.groupList,
        'backToGroupList'
      );
      return () => {
        // remove event listener on unmount
        backHandlerHelper.removeBackHandler(BackHandler, 'backToGroupList');
      };
    }, [])
  );

  return (
    <View>
      <Text>{groupId}</Text>
      <Button
        title="Add Friends"
        onPress={() => {
          navigation.navigate(ScreenNames.friendsForGroup, {
            groupId,
            name: groupName,
          });
        }}
      />
    </View>
  );
};

export default GroupDetailsScreen;
