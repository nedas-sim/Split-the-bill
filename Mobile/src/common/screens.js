import MainScreen from "../screens/MainScreen";
import RegisterScreen from "../screens/RegisterScreen";
import GroupListScreen from "../screens/GroupListScreen";

export const Screens = {
  mainScreen: {
    name: "Main Screen",
    component: MainScreen,
  },
  registration: {
    name: "Registration",
    component: RegisterScreen,
  },
  groupList: {
    name: "Groups",
    component: GroupListScreen,
  },
};
