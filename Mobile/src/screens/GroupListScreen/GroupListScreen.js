import React from 'react';
import groupService from '../../services/groupService';
import ScreenNames from '../../common/screenNames';
import GenericListScreen from '../../features/genericList/GenericListScreen/GenericListScreen';
import GroupListItem from '../../features/group/GroupListItem/GroupListItem';

const GroupListScreen = ({ navigation }) => (
  <GenericListScreen
    fetchItems={groupService.getGroups}
    renderItem={(group) => (
      <GroupListItem key={group.groupId} group={group} navigation={navigation} />
    )}
    noItemsMessages={["There aren't any groups that you are a member of", '😔']}
    onAddNew={() => navigation.navigate(ScreenNames.createGroupScreen)}
    emptySearch
    searchEnabled
  />
);

export default GroupListScreen;
