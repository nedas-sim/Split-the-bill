import React from 'react';
import { Button } from 'react-native';
import authService from '../../../services/authService';
import ScreenNames from '../../../common/screenNames';

const LogoutButton = ({ navigation }) => {
  const handleLogoutButtonPress = async () => {
    await authService.logout();
    navigation.navigate(ScreenNames.mainScreen);
  };

  return <Button title="Logout" onPress={handleLogoutButtonPress} />;
};

export default LogoutButton;
