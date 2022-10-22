import React, { useState } from 'react';
import { SafeAreaView, View, Button, Alert, ActivityIndicator } from 'react-native';
import authService from '../../services/authService';
import ScreenNames from '../../common/screenNames';
import styles from './styles';
import EmailInput from '../../components/EmailInput/EmailInput';
import PasswordInput from '../../components/PasswordInput/PasswordInput';

const RegisterScreen = ({ navigation }) => {
  const [credentials, setCredentials] = useState({
    email: '',
    password: '',
    repeatPassword: '',
  });
  const [loading, setLoading] = useState(false);

  const handleRegisterButtonPress = async () => {
    try {
      setLoading(true);
      await authService.register(credentials);
      Alert.alert('Success', 'Registration successful!', [
        {
          text: 'Login',
          onPress: () => navigation.navigate(ScreenNames.mainScreen),
        },
      ]);
    } catch (ex) {
      Alert.alert('Error', ex.response.data.message);
    } finally {
      setLoading(false);
    }
  };

  return (
    <SafeAreaView style={styles.screen}>
      <View style={styles.inputsContainer}>
        <EmailInput
          value={credentials.email}
          onChange={(email) => setCredentials((creds) => ({ ...creds, email }))}
        />
        <PasswordInput
          value={credentials.password}
          onChange={(password) => setCredentials((creds) => ({ ...creds, password }))}
        />
        <PasswordInput
          value={credentials.repeatPassword}
          onChange={(repeatPassword) => setCredentials((creds) => ({ ...creds, repeatPassword }))}
          placeholder="Repeat password"
        />
      </View>
      <Button style={styles.button} title="Register" onPress={handleRegisterButtonPress} />
      {loading && <ActivityIndicator size="large" />}
    </SafeAreaView>
  );
};

export default RegisterScreen;
