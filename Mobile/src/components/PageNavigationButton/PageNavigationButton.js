import React from 'react';
import { View, TouchableOpacity, Text } from 'react-native';
import styles from './styles';

const PageNavigationButton = ({ enabled, text, onClick }) => {
  const textStyle = {
    ...styles.text,
    opacity: enabled ? 1 : 0.2,
  };

  const handleClick = () => {
    if (enabled) onClick();
  };

  return (
    <TouchableOpacity
      style={styles.container}
      activeOpacity={enabled ? 0 : 1}
      onPress={handleClick}
    >
      <View>
        <Text style={textStyle}>{text}</Text>
      </View>
    </TouchableOpacity>
  );
};

export default PageNavigationButton;
