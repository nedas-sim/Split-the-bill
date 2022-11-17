import React from 'react';
import { ScrollView, SafeAreaView } from 'react-native';
import UserListItem from '../UserListItem/UserListItem';
import styles from './styles';
import StaticTextArea from '../../../components/StaticTextArea/StaticTextArea';

const UserList = ({ users, fetch }) => (
  <SafeAreaView style={styles.container}>
    {users?.length > 0 ? (
      <ScrollView style={styles.userListContainer}>
        {users?.map((user) => (
          <UserListItem key={user.id} user={user} fetch={fetch} />
        ))}
      </ScrollView>
    ) : (
      <StaticTextArea texts={['No users found', 'Type username or email to find someone!']} />
    )}
  </SafeAreaView>
);

export default UserList;
