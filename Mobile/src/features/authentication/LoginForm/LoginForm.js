import React, { useState } from 'react';
import { SafeAreaView, TextInput, Button, Alert, ActivityIndicator } from 'react-native';
import authService from '../../../services/authService';
import { Screens } from '../../../common/screens';
import styles from './styles';

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
      <TextInput
        style={styles.input}
        placeholder="Email"
        placeholderTextColor="#fff"
        keyboardType="email-address"
        autoCapitalize="none"
        value={credentials.email}
        onChangeText={(email) => setCredentials((creds) => ({ ...creds, email }))}
      />
      <TextInput
        style={styles.input}
        placeholder="Password"
        placeholderTextColor="#fff"
        secureTextEntry
        autoCapitalize="none"
        value={credentials.password}
        onChangeText={(password) => setCredentials((creds) => ({ ...creds, password }))}
      />
      <Button title="Login" onPress={handleLoginButtonPress} />
      {loading && <ActivityIndicator size="large" />}
    </SafeAreaView>
  );
};

export default LoginForm;
