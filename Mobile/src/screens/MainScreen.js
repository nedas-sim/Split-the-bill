import React from "react";
import { Text, SafeAreaView, View, Button, StyleSheet } from "react-native";
import LoginForm from "../components/LoginForm";

const MainScreen = () => {
  /*return (
    <SafeAreaView style={styles.screen}>
      <View style={styles.container}>
        <Text style={styles.appName}>Split The Bill</Text>
        <View style={styles.buttonContainer}>
          <Button style={[styles.button, styles.login]} title="Login" />
          <Button style={[styles.button, styles.register]} title="Register" />
        </View>
      </View>
    </SafeAreaView>
  );*/
  return (
    <View style={styles.screen}>
      <View style={styles.container}>
        <Text style={styles.appName}>Split The Bill</Text>
        <LoginForm />
        <View style={styles.extraButtonsContainer}></View>
      </View>
    </View>
  );
};

const debuging1 = { borderColor: "red", borderWidth: 1 };
const debuging2 = { borderColor: "green", borderWidth: 1 };

const styles = StyleSheet.create({
  screen: {
    width: "100%",
    height: "100%",
    backgroundColor: "#260E63",
    alignItems: "center",
    justifyContent: "center",
  },
  container: {
    ...debuging1,
    width: "80%",
    height: "50%",
    alignItems: "center",
  },
  appName: {
    //...debuging2,
    color: "#fff",
    height: "30%",
    fontSize: 35,
  },
});

export default MainScreen;
