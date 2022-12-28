import React, { useContext } from 'react';
import { Alert, Button } from 'react-native';
import groupService from '../../../services/groupService';
import CoreGroupListItem from '../../../components/CoreGroupListItem/CoreGroupListItem';
import { RefetchContext } from '../../../common/context';

const GroupInvitationItem = ({ group }) => {
  const retrieveInvitations = useContext(RefetchContext);
  const handleGroupInvitationInteraction = async (isAccepted) => {
    try {
      const body = {
        groupId: group.groupId,
        isAccepted,
      };
      await groupService.updateInvitation(body);
      await retrieveInvitations();
    } catch (ex) {
      Alert.alert('Error', ex.response.data.message);
    }
  };
  return (
    <CoreGroupListItem
      group={group}
      showButtonContainer
      renderButtonContainer={() => (
        <>
          <Button title="Accept" onPress={() => handleGroupInvitationInteraction(true)} />
          <Button title="Reject" onPress={() => handleGroupInvitationInteraction(false)} />
        </>
      )}
    />
  );
};

export default GroupInvitationItem;
