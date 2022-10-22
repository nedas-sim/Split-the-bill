import MainScreen from '../screens/MainScreen/MainScreen';
import RegisterScreen from '../screens/RegisterScreen/RegisterScreen';
import GroupListScreen from '../screens/GroupListScreen/GroupListScreen';
import LogoutButton from '../features/authentication/LogoutButton/LogoutButton';
import ScreenNames from './screenNames';

export const Screens = {
  mainScreen: {
    name: ScreenNames.mainScreen,
    component: MainScreen,
    options: {
      headerBackVisible: false,
    },
  },
  registration: {
    name: ScreenNames.registration,
    component: RegisterScreen,
    options: {},
  },
  groupList: {
    name: ScreenNames.groupList,
    component: GroupListScreen,
    options: ({ navigation }) => ({
      headerBackVisible: false,
      headerRight: () => <LogoutButton navigation={navigation} />,
    }),
  },
};
