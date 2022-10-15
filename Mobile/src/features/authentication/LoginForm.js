import React from "react";
import { SafeAreaView, StyleSheet, TextInput, Button } from "react-native";

const LoginForm = () => {
  return (
    <SafeAreaView style={styles.container}>
      <TextInput
        style={styles.input}
        placeholder="Email"
        placeholderTextColor="#fff"
        keyboardType="email-address"
        autoCapitalize="none"
      />
      <TextInput
        style={styles.input}
        placeholder="Password"
        placeholderTextColor="#fff"
        secureTextEntry
        autoCapitalize="none"
      />
      <Button title="Login" />
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
