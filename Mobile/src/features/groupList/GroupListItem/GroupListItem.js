import React from 'react';
import { View, Text, TouchableOpacity } from 'react-native';
import styles from './styles';

const GroupListItem = ({ group }) => {
  return (
    <TouchableOpacity style={styles.card}>
      <Text style={styles.groupName}>{group.groupName}</Text>
    </TouchableOpacity>
  );
};

export default GroupListItem;
