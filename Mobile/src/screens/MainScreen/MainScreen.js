import React, { useEffect } from 'react';
import { Text, View, Button } from 'react-native';
import LoginForm from '../../features/authentication/LoginForm/LoginForm';
import ScreenNames from '../../common/screenNames';
import authService from '../../services/authService';
import styles from './styles';

const MainScreen = ({ navigation }) => {
  useEffect(() => {
    const redirectIfLoggedIn = async () => {
      try {
        await authService.isLoggedIn();
        navigation.navigate(ScreenNames.groupList);
      } catch {}
    };

    redirectIfLoggedIn();
  }, []);

  return (
    <View style={styles.screen}>
      <View style={styles.container}>
        <Text style={styles.appName}>Split The Bill</Text>
        <LoginForm navigation={navigation} />
        <View style={styles.extraButtonsContainer}>
          <Button title="Register" onPress={() => navigation.navigate(ScreenNames.registration)} />
        </View>
      </View>
    </View>
  );
};

export default MainScreen;
