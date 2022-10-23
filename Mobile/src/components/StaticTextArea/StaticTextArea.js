import React from 'react';
import { Text, View } from 'react-native';
import styles from './styles';

const StaticTextArea = ({ texts }) => {
  return (
    <View style={styles.container}>
      {texts.map((text) => (
        <Text key={text} style={styles.text}>
          {text}
        </Text>
      ))}
    </View>
  );
};

export default StaticTextArea;
