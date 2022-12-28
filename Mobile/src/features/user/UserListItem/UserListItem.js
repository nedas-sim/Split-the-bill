import React, { useContext } from 'react';
import { Button, Alert } from 'react-native';
import friendService from '../../../services/friendService';
import { RefetchContext } from '../../../common/context';
import CoreUserListItem from '../../../components/CoreUserListItem/CoreUserListItem';

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

  return (
    <CoreUserListItem
      showStatus
      user={user}
      showButtonContainer={user.canInvite || user.canAccept}
      renderButtonContainer={() => (
        <>
          {user.canInvite && <Button title="Invite" onPress={handleSendFriendRequest} />}
          {user.canAccept && (
            <>
              <Button title="Accept" onPress={() => handleFriendRequestInteraction(true)} />
              <Button title="Reject" onPress={() => handleFriendRequestInteraction(false)} />
            </>
          )}
        </>
      )}
    />
  );
};

export default UserListItem;
