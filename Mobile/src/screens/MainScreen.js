import React from "react";
import { Text, View, Button, StyleSheet } from "react-native";
import LoginForm from "../features/authentication/LoginForm";

const MainScreen = () => {
  return (
    <View style={styles.screen}>
      <View style={styles.container}>
        <Text style={styles.appName}>Split The Bill</Text>
        <LoginForm />
        <View style={styles.extraButtonsContainer}>
          <Button title="Register" />
        </View>
      </View>
    </View>
  );
};

const styles = StyleSheet.create({
  screen: {
    width: "100%",
    height: "100%",
    backgroundColor: "#260E63",
    alignItems: "center",
    justifyContent: "center",
  },
  container: {
    width: "80%",
    height: "50%",
    alignItems: "center",
  },
  appName: {
    color: "#fff",
    height: "30%",
    fontSize: 35,
  },
  extraButtonsContainer: {
    width: "70%",
    marginTop: 25,
  },
});

export default MainScreen;
