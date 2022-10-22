import React, { useEffect } from 'react';
import { Text, View, Button, StyleSheet } from 'react-native';
import LoginForm from '../features/authentication/LoginForm';
import { Screens } from '../common/screens';
import authService from '../services/authService';

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

const styles = StyleSheet.create({
  screen: {
    width: '100%',
    height: '100%',
    backgroundColor: '#260E63',
    alignItems: 'center',
    justifyContent: 'center',
  },
  container: {
    width: '80%',
    height: '50%',
    alignItems: 'center',
  },
  appName: {
    color: '#fff',
    height: '30%',
    fontSize: 35,
  },
  extraButtonsContainer: {
    width: '70%',
    marginTop: 25,
  },
});

export default MainScreen;
