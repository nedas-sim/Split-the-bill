import MainScreen from '../screens/MainScreen/MainScreen';
import RegisterScreen from '../screens/RegisterScreen/RegisterScreen';
import GroupListScreen from '../screens/GroupListScreen/GroupListScreen';
import LogoutButton from '../features/authentication/LogoutButton/LogoutButton';

export const Screens = {
  mainScreen: {
    name: 'Main Screen',
    component: MainScreen,
    options: {
      headerBackVisible: false,
    },
  },
  registration: {
    name: 'Registration',
    component: RegisterScreen,
    options: {},
  },
  groupList: {
    name: 'Groups',
    component: GroupListScreen,
    options: ({ navigation }) => ({
      headerBackVisible: false,
      headerRight: () => <LogoutButton navigation={navigation} />,
    }),
  },
};
