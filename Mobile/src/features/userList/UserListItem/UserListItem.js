import React from 'react';
import { Text, TouchableOpacity } from 'react-native';
import styles from './styles';

const UserListItem = ({ user }) => (
  <TouchableOpacity style={styles.card}>
    <Text style={styles.username}>{user.username}</Text>
  </TouchableOpacity>
);

export default UserListItem;
