import React from 'react';
import { Text, TouchableOpacity, View } from 'react-native';
import styles from './styles';

const CoreUserListItem = ({ user, showButtonContainer, renderButtonContainer, showStatus }) => (
  <TouchableOpacity style={styles.card}>
    <View>
      <Text style={styles.username}>{user.username}</Text>
      {showStatus && <Text style={styles.status}>{user.status}</Text>}
    </View>
    {showButtonContainer && <View style={styles.buttonContainer}>{renderButtonContainer()}</View>}
  </TouchableOpacity>
);

export default CoreUserListItem;
