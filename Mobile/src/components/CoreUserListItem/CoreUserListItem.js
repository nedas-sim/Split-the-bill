import React from 'react';
import { Text, TouchableOpacity, View } from 'react-native';
import styles from './styles';

const CoreUserListItem = (props) => {
  const { user, showButtonContainer, renderButtonContainer, showStatus, onCardPress } = props;
  return (
    <TouchableOpacity style={styles.card} onPress={onCardPress}>
      <View>
        <Text style={styles.username}>{user.username}</Text>
        {showStatus && <Text style={styles.status}>{user.status}</Text>}
      </View>
      {showButtonContainer && <View style={styles.buttonContainer}>{renderButtonContainer()}</View>}
    </TouchableOpacity>
  );
};

export default CoreUserListItem;
