let cachedAction;

const setupBackHandler = (BackHandler, Alert) => {
  const action = () => {
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
  };

  cachedAction = action;

  BackHandler.addEventListener('hardwareBackPress', cachedAction);
};

const removeBackHandler = (BackHandler) => {
  BackHandler.removeEventListener('hardwareBackPress', cachedAction);
};

const backHandlerHelper = {
  setupBackHandler,
  removeBackHandler,
};

export default backHandlerHelper;
