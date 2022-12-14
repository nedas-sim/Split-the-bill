import React, { useEffect } from 'react';
import { View, Button, BackHandler, Alert } from 'react-native';
import { useIsFocused } from '@react-navigation/native';
import backHandlerHelper from '../../common/backHandlerHelper';
import styles from './styles';
import ScreenNames from '../../common/screenNames';

const MainScreen = ({ navigation }) => {
  const isFocused = useIsFocused();

  const buttons = [
    {
      title: 'Groups',
      redirectTo: ScreenNames.groupList,
    },
    {
      title: 'Users',
      redirectTo: ScreenNames.userList,
    },
    {
      title: 'Friend Requests',
      redirectTo: ScreenNames.friendRequests,
    },
    {
      title: 'Friends',
      redirectTo: ScreenNames.friendList,
    },
    {
      title: 'Profile',
      redirectTo: ScreenNames.profile,
    },
    {
      title: 'Group Invitations',
      redirectTo: ScreenNames.groupInvitations,
    },
  ];

  useEffect(() => {
    if (isFocused) {
      backHandlerHelper.setExitListener(BackHandler, Alert, 'exitPress');
    } else {
      backHandlerHelper.removeBackHandler(BackHandler, 'exitPress');
    }
  }, [isFocused]);

  return (
    <View style={styles.screen}>
      {buttons.map((btn) => (
        <Button
          key={btn.title}
          title={btn.title}
          onPress={() => navigation.navigate(btn.redirectTo)}
        />
      ))}
    </View>
  );
};

export default MainScreen;
