import React, { useContext } from 'react';
import { Text, TouchableOpacity, Button, View, Alert } from 'react-native';
import styles from './styles';
import friendService from '../../../services/friendService';
import { RefetchContext } from '../../../common/context';

const UserListItem = ({ user }) => {
  const retrieveUsers = useContext(RefetchContext);

  const handleSendFriendRequest = async () => {
    try {
      const body = {
        receivingUserId: user.id,
      };
      await friendService.sendFriendRequest(body);
      await retrieveUsers();
    } catch (ex) {
      Alert.alert('Error', ex.response.data.message);
    }
  };

  const handleFriendRequestInteraction = async (isAccepted) => {
    try {
      const body = {
        senderId: user.id,
        isAccepted,
      };
      await friendService.interactWithFriendRequest(body);
      await retrieveUsers();
    } catch (ex) {
      Alert.alert('Error', ex.response.data.message);
    }
  };

  const showButtonContainer = user.canInvite || user.canAccept;

  return (
    <TouchableOpacity style={styles.card}>
      <View>
        <Text style={styles.username}>{user.username}</Text>
        <Text style={styles.status}>{user.status}</Text>
      </View>
      {showButtonContainer && (
        <View style={styles.buttonContainer}>
          {user.canInvite && <Button title="Invite" onPress={handleSendFriendRequest} />}
          {user.canAccept && (
            <>
              <Button title="Accept" onPress={() => handleFriendRequestInteraction(true)} />
              <Button title="Reject" onPress={() => handleFriendRequestInteraction(false)} />
            </>
          )}
        </View>
      )}
    </TouchableOpacity>
  );
};

export default UserListItem;
