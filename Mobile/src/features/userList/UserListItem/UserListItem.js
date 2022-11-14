import React from 'react';
import { Text, TouchableOpacity, Button, View } from 'react-native';
import styles from './styles';

const UserListItem = ({ user }) => (
  <TouchableOpacity style={styles.card}>
    <View>
      <Text style={styles.username}>{user.username}</Text>
      <Text style={styles.status}>{user.status}</Text>
    </View>
    {user.canInvite && (
      <View style={styles.buttonContainer}>
        <Button title="Invite" />
      </View>
    )}
  </TouchableOpacity>
);

export default UserListItem;
