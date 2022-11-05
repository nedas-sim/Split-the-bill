import React from 'react';
import { TextInput } from 'react-native';
import styles from './styles';

const CoreInput = (props) => (
  <TextInput style={styles.input} placeholderTextColor="#fff" autoCapitalize="none" {...props} />
);

export default CoreInput;
