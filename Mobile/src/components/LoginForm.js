import React from "react";
import { SafeAreaView, StyleSheet, TextInput, Button } from "react-native";

const LoginForm = () => {
  return (
    <SafeAreaView style={styles.container}>
      <TextInput style={styles.input} placeholder="Email" />
      <TextInput style={styles.input} placeholder="Password" secureTextEntry />
      <Button title="Login" />
    </SafeAreaView>
  );
};

const debuging3 = { borderColor: "green", borderWidth: 1 };

const styles = StyleSheet.create({
  container: {
    ...debuging3,
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
