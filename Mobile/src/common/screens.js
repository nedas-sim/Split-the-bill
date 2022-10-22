import MainScreen from '../screens/MainScreen';
import RegisterScreen from '../screens/RegisterScreen';
import GroupListScreen from '../screens/GroupListScreen';
import LogoutButton from '../features/authentication/LogoutButton';

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
