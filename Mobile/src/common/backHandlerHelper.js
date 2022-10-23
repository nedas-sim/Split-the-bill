const setupBackHandler = (BackHandler, Alert) => {
  BackHandler.addEventListener('hardwareBackPress', () => {
    Alert.alert(
      'Exit',
      'Do you want to exit the app?',
      [
        {
          text: 'No',
        },
        {
          text: 'Yes',
          onPress: () => BackHandler.exitApp(),
        },
      ],
      {
        cancelable: false,
      }
    );
    return true;
  });
};

export default setupBackHandler;
