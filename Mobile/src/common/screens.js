import ScreenNames from './screenNames';
import MainScreen from '../screens/MainScreen/MainScreen';
import RegisterScreen from '../screens/RegisterScreen/RegisterScreen';
import GroupListScreen from '../screens/GroupListScreen/GroupListScreen';
import LogoutButton from '../features/authentication/LogoutButton/LogoutButton';
import CreateGroupScreen from '../screens/CreateGroupScreen/CreateGroupScreen';
import GroupDetailsScreen from '../screens/GroupDetailsScreen/GroupDetailsScreen';

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
  createGroupScreen: {
    name: ScreenNames.createGroupScreen,
    component: CreateGroupScreen,
    options: ({ navigation }) => ({
      headerRight: () => <LogoutButton navigation={navigation} />,
    }),
  },
  groupDetailsScreen: {
    name: ScreenNames.groupDetailsScreen,
    component: GroupDetailsScreen,
    options: ({ navigation }) => ({
      headerRight: () => <LogoutButton navigation={navigation} />,
    }),
  },
};
