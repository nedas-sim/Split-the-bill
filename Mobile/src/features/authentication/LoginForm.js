import React, { useState } from "react";
import {
  SafeAreaView,
  StyleSheet,
  TextInput,
  Button,
  Alert,
  ActivityIndicator,
} from "react-native";
import authService from "../../services/authService";
import { Screens } from "../../common/screens";

const LoginForm = ({ navigation }) => {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");

  const [loading, setLoading] = useState(false);

  const handleLoginButtonPress = async () => {
    try {
      setLoading(true);
      await authService.login({ email, password });
      navigation.navigate(Screens.groupList.name);
    } catch (ex) {
      Alert.alert("Error", ex.response.data.message);
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
      <Button title="Login" onPress={handleLoginButtonPress} />
      {loading && <ActivityIndicator size="large" />}
    </SafeAreaView>
  );
};

const styles = StyleSheet.create({
  container: {
    width: "70%",
    height: "40%",
    flexDirection: "column",
    justifyContent: "space-between",
  },
  input: {
    backgroundColor: "#453176",
    width: "100%",
    color: "#fff",
  },
});

export default LoginForm;
