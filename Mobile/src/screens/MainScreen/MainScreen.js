import React, { useEffect } from 'react';
import { Text, View, Button } from 'react-native';
import LoginForm from '../features/authentication/LoginForm';
import { Screens } from '../common/screens';
import authService from '../services/authService';
import styles from './styles';

const MainScreen = ({ navigation }) => {
  useEffect(() => {
    const redirectIfLoggedIn = async () => {
      try {
        await authService.isLoggedIn();
        navigation.navigate(Screens.groupList.name);
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
          <Button title="Register" onPress={() => navigation.navigate(Screens.registration.name)} />
        </View>
      </View>
    </View>
  );
};

export default MainScreen;
