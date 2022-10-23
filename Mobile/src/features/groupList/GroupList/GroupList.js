import React from 'react';
import { ScrollView, SafeAreaView, Text } from 'react-native';
import GroupListItem from '../GroupListItem/GroupListItem';
import styles from './styles';
import StaticTextArea from '../../../components/StaticTextArea/StaticTextArea';

const GroupList = ({ groups, navigation }) => {
  return (
    <SafeAreaView style={styles.container}>
      {groups?.length > 0 ? (
        <ScrollView style={styles.groupListContainer}>
          {groups?.map((g) => (
            <GroupListItem key={g.groupId} group={g} navigation={navigation} />
          ))}
        </ScrollView>
      ) : (
        <StaticTextArea texts={["There aren't any groups that you are a member of", '😔']} />
      )}
    </SafeAreaView>
  );
};

export default GroupList;
