import React from 'react';
import CoreUserListItem from '../../../components/CoreUserListItem/CoreUserListItem';
import InviteToGroupButton from '../../groupList/InviteToGroupButton/InviteToGroupButton';

const FriendForGroupItem = ({ user }) => (
  <CoreUserListItem
    user={user}
    showButtonContainer
    renderButtonContainer={() => <InviteToGroupButton userId={user.id} />}
  />
);

export default FriendForGroupItem;
