import React, { useState } from 'react';
import { SafeAreaView, Button, Alert, ActivityIndicator, View } from 'react-native';
import ScreenNames from '../../../common/screenNames';
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
      navigation.navigate(ScreenNames.groupList);
    } catch (ex) {
      Alert.alert('Error', ex.response.data.message);
    } finally {
      setLoading(false);
    }
  };

  return (
    <SafeAreaView style={styles.container}>
      <View style={styles.inputContainer}>
        <EmailInput
          value={credentials.email}
          onChange={(email) => setCredentials((creds) => ({ ...creds, email }))}
        />
      </View>
      <View style={styles.inputContainer}>
        <PasswordInput
          value={credentials.password}
          onChange={(password) => setCredentials((creds) => ({ ...creds, password }))}
        />
      </View>
      <Button title="Login" onPress={handleLoginButtonPress} />
      {loading && <ActivityIndicator size="large" />}
    </SafeAreaView>
  );
};

export default LoginForm;
