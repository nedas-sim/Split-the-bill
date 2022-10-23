let cachedAction = {};

const setExitListener = (BackHandler, Alert, listenerName) => {
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

  //cachedAction = action;
  cachedAction[listenerName] = action;

  BackHandler.addEventListener(listenerName, cachedAction[listenerName]);
};

const removeBackHandler = (BackHandler, listenerName) => {
  BackHandler.removeEventListener(listenerName, cachedAction[listenerName]);
};

const setBackScreen = (BackHandler, navigation, screenName, listenerName) => {
  const action = () => {
    navigation.navigate(screenName);
    return true;
  };

  cachedAction[listenerName] = action;

  BackHandler.addEventListener(listenerName, cachedAction[listenerName]);
};

const backHandlerHelper = {
  setExitListener,
  removeBackHandler,
  setBackScreen,
};

export default backHandlerHelper;
