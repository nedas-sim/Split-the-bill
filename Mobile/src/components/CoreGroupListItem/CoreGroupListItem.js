import React from 'react';
import { Text, TouchableOpacity, View } from 'react-native';
import styles from './styles';

const CoreGroupListItem = (props) => {
  const { group, onCardPress, showButtonContainer, renderButtonContainer } = props;
  return (
    <TouchableOpacity style={styles.card} onPress={onCardPress}>
      <View>
        <Text style={styles.groupName}>{group.groupName}</Text>
      </View>
      {showButtonContainer && <View style={styles.buttonContainer}>{renderButtonContainer()}</View>}
    </TouchableOpacity>
  );
};

export default CoreGroupListItem;
