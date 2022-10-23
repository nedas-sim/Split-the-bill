import React, { useCallback } from 'react';
import { Text, View, BackHandler } from 'react-native';
import { useFocusEffect } from '@react-navigation/native';
import ScreenNames from '../../common/screenNames';
import backHandlerHelper from '../../common/backHandlerHelper';

const GroupDetailsScreen = ({ route, navigation }) => {
  const { groupId } = route.params;

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
    </View>
  );
};

export default GroupDetailsScreen;
