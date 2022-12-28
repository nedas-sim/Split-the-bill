import React from 'react';
import CoreGroupListItem from '../../../components/CoreGroupListItem/CoreGroupListItem';
import InviteToGroupButton from '../InviteToGroupButton/InviteToGroupButton';

const GroupForFriendItem = ({ group }) => (
  <CoreGroupListItem
    group={group}
    showButtonContainer
    renderButtonContainer={() => <InviteToGroupButton groupId={group.groupId} />}
  />
);

export default GroupForFriendItem;
