import React from 'react';
import { Text, TouchableOpacity, Button, View, Alert } from 'react-native';
import styles from './styles';
import friendService from '../../../services/friendService';

const UserListItem = ({ user }) => {
  const handleSendFriendRequest = async () => {
    try {
      const body = {
        receivingUserId: user.id,
      };
      await friendService.sendFriendRequest(body);
    } catch (ex) {
      Alert.alert('Error', ex.response.data.message);
    }
  };

  return (
    <TouchableOpacity style={styles.card}>
      <View>
        <Text style={styles.username}>{user.username}</Text>
        <Text style={styles.status}>{user.status}</Text>
      </View>
      {user.canInvite && (
        <View style={styles.buttonContainer}>
          <Button title="Invite" onPress={handleSendFriendRequest} />
        </View>
      )}
    </TouchableOpacity>
  );
};

export default UserListItem;
