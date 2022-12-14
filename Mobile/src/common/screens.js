import ScreenNames from './screenNames';
import LoginScreen from '../screens/LoginScreen/LoginScreen';
import MainScreen from '../screens/MainScreen/MainScreen';
import RegisterScreen from '../screens/RegisterScreen/RegisterScreen';
import GroupListScreen from '../screens/GroupListScreen/GroupListScreen';
import LogoutButton from '../features/authentication/LogoutButton/LogoutButton';
import CreateGroupScreen from '../screens/CreateGroupScreen/CreateGroupScreen';
import GroupDetailsScreen from '../screens/GroupDetailsScreen/GroupDetailsScreen';
import UserListScreen from '../screens/UserListScreen/UserListScreen';
import FriendRequestScreen from '../screens/FriendRequestScreen/FriendRequestScreen';
import FriendListScreen from '../screens/FriendListScreen/FriendListScreen';
import UserProfileScreen from '../screens/UserProfileScreen/UserProfileScreen';
import FriendsForGroupScreen from '../screens/FriendsForGroupScreen/FriendsForGroupScreen';
import FriendDetailsScreen from '../screens/FriendDetailsScreen/FriendDetailsScreen';
import GroupsForFriendScreen from '../screens/GroupsForFriendScreen/GroupsForFriendScreen';
import GroupInvitationsScreen from '../screens/GroupInvitationsScreen/GroupInvitationsScreen';

const Screens = {
  loginScreen: {
    name: ScreenNames.loginScreen,
    component: LoginScreen,
    options: {
      headerBackVisible: false,
    },
  },
  mainScreen: {
    name: ScreenNames.mainScreen,
    component: MainScreen,
    options: ({ navigation }) => ({
      headerBackVisible: false,
      headerRight: () => <LogoutButton navigation={navigation} />,
    }),
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
    options: ({ navigation, route }) => ({
      headerRight: () => <LogoutButton navigation={navigation} />,
      title: route.params.groupName,
    }),
  },
  userListScreen: {
    name: ScreenNames.userList,
    component: UserListScreen,
    options: ({ navigation }) => ({
      headerRight: () => <LogoutButton navigation={navigation} />,
    }),
  },
  friendRequestsScreen: {
    name: ScreenNames.friendRequests,
    component: FriendRequestScreen,
    options: ({ navigation }) => ({
      headerRight: () => <LogoutButton navigation={navigation} />,
    }),
  },
  friendListScreen: {
    name: ScreenNames.friendList,
    component: FriendListScreen,
    options: ({ navigation }) => ({
      headerRight: () => <LogoutButton navigation={navigation} />,
    }),
  },
  profileScreen: {
    name: ScreenNames.profile,
    component: UserProfileScreen,
    options: ({ navigation }) => ({
      headerRight: () => <LogoutButton navigation={navigation} />,
    }),
  },
  friendsForGroupScreen: {
    name: ScreenNames.friendsForGroup,
    component: FriendsForGroupScreen,
    options: ({ navigation, route }) => ({
      headerRight: () => <LogoutButton navigation={navigation} />,
      title: `Invite to ${route.params.name}`,
    }),
  },
  friendDetailsScreen: {
    name: ScreenNames.friendDetailsScreen,
    component: FriendDetailsScreen,
    options: ({ navigation, route }) => ({
      headerRight: () => <LogoutButton navigation={navigation} />,
      title: route.params.userName,
    }),
  },
  groupForFriend: {
    name: ScreenNames.groupsForFriend,
    component: GroupsForFriendScreen,
    options: ({ navigation, route }) => ({
      headerRight: () => <LogoutButton navigation={navigation} />,
      title: `Invite ${route.params.name}`,
    }),
  },
  groupInvitations: {
    name: ScreenNames.groupInvitations,
    component: GroupInvitationsScreen,
    options: ({ navigation }) => ({
      headerRight: () => <LogoutButton navigation={navigation} />,
    }),
  },
};

export default Screens;
