import React from 'react';
import { Button } from 'react-native';
import authService from '../../../services/authService';
import { Screens } from '../../../common/screens';

const LogoutButton = ({ navigation }) => {
  const handleLogoutButtonPress = async () => {
    await authService.logout();
    navigation.navigate(Screens.mainScreen.name);
  };

  return <Button title="Logout" onPress={handleLogoutButtonPress} />;
};

export default LogoutButton;
