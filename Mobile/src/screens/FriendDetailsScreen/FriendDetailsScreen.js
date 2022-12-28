import React, { useCallback } from 'react';
import { Text, View, BackHandler, Button } from 'react-native';
import { useFocusEffect } from '@react-navigation/native';
import ScreenNames from '../../common/screenNames';
import backHandlerHelper from '../../common/backHandlerHelper';

const FriendDetailsScreen = ({ route, navigation }) => {
  const { userId, userName } = route.params;

  useFocusEffect(
    useCallback(() => {
      // setup event listener on mount
      backHandlerHelper.setBackScreen(
        BackHandler,
        navigation,
        ScreenNames.friendList,
        'backToFriendList'
      );
      return () => {
        // remove event listener on unmount
        backHandlerHelper.removeBackHandler(BackHandler, 'backToFriendList');
      };
    }, [])
  );
  return (
    <View>
      <Text>{userId}</Text>
      <Button
        title="Invite to group"
        onPress={() => {
          navigation.navigate(ScreenNames.groupsForFriend, {
            userId,
            name: userName,
          });
        }}
      />
    </View>
  );
};

export default FriendDetailsScreen;
