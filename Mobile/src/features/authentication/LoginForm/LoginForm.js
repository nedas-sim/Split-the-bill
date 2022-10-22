import React, { useState } from 'react';
import { SafeAreaView, Button, Alert, ActivityIndicator } from 'react-native';
import { Screens } from '../../../common/screens';
import styles from './styles';
import authService from '../../../services/authService';
import EmailInput from '../../../components/EmailInput/EmailInput';
import PasswordInput from '../../../components/PasswordInput/PasswordInput';

const LoginForm = ({ navigation }) => {
  const [credentials, setCredentials] = useState({
    email: '',
    password: '',
  });
  const [loading, setLoading] = useState(false);

  const handleLoginButtonPress = async () => {
    try {
      setLoading(true);
      await authService.login(credentials);
      navigation.navigate(Screens.groupList.name);
    } catch (ex) {
      Alert.alert('Error', ex.response.data.message);
    } finally {
      setLoading(false);
    }
  };

  return (
    <SafeAreaView style={styles.container}>
      <EmailInput
        value={credentials.email}
        onChange={(email) => setCredentials((creds) => ({ ...creds, email }))}
      />
      <PasswordInput
        value={credentials.password}
        onChange={(password) => setCredentials((creds) => ({ ...creds, password }))}
      />
      <Button title="Login" onPress={handleLoginButtonPress} />
      {loading && <ActivityIndicator size="large" />}
    </SafeAreaView>
  );
};

export default LoginForm;
