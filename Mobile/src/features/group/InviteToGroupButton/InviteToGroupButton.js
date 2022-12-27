import React, { useContext } from 'react';
import { Button, Alert } from 'react-native';
import { RefetchContext, GroupInvitationContext } from '../../../common/context';
import groupService from '../../../services/groupService';

const InviteToGroupButton = ({ groupId, userId }) => {
  const retrieveUsers = useContext(RefetchContext);
  const invitationInfo = useContext(GroupInvitationContext);

  const handleButtonPress = async () => {
    try {
      const body = {
        receivingUserId: userId || invitationInfo.userId,
        groupId: groupId || invitationInfo.groupId,
      };
      await groupService.sendInvitationToGroup(body);
      await retrieveUsers();
    } catch (ex) {
      Alert.alert('Error', ex.response.data.message);
    }
  };
  return <Button title="Invite" onPress={handleButtonPress} />;
};

export default InviteToGroupButton;
