import React, { useState } from 'react';
import {
  SafeAreaView,
  StyleSheet,
  View,
  Button,
  TextInput,
  Alert,
  ActivityIndicator,
} from 'react-native';
import authService from '../services/authService';
import { Screens } from '../common/screens';

const RegisterScreen = ({ navigation }) => {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [repeatPassword, setRepeatPassword] = useState('');
  const [loading, setLoading] = useState(false);

  const handleRegisterButtonPress = async () => {
    try {
      setLoading(true);
      await authService.register({ email, password, repeatPassword });
      Alert.alert('Success', 'Registration successful!', [
        {
          text: 'Login',
          onPress: () => navigation.navigate(Screens.mainScreen.name),
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
        <TextInput
          style={styles.input}
          placeholder="Email"
          placeholderTextColor="#fff"
          keyboardType="email-address"
          autoCapitalize="none"
          value={email}
          onChangeText={(email) => setEmail(email)}
        />
        <TextInput
          style={styles.input}
          placeholder="Password"
          placeholderTextColor="#fff"
          secureTextEntry
          autoCapitalize="none"
          value={password}
          onChangeText={(password) => setPassword(password)}
        />
        <TextInput
          style={styles.input}
          placeholder="Repeat password"
          placeholderTextColor="#fff"
          secureTextEntry
          autoCapitalize="none"
          value={repeatPassword}
          onChangeText={(repeatPassword) => setRepeatPassword(repeatPassword)}
        />
      </View>
      <Button style={styles.button} title="Register" onPress={handleRegisterButtonPress} />
      {loading && <ActivityIndicator size="large" />}
    </SafeAreaView>
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
  inputsContainer: {
    width: '70%',
    height: '20%',
    flexDirection: 'column',
    justifyContent: 'space-between',
    marginBottom: 40,
  },
  input: {
    backgroundColor: '#453176',
    width: '100%',
    color: '#fff',
  },
});

export default RegisterScreen;
