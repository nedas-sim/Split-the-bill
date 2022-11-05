import React from 'react';
import CoreInput from '../CoreInput/CoreInput';

const defaultPlaceholder = 'Password';

const PasswordInput = ({ value, onChange, placeholder }) => (
  <CoreInput
    placeholder={placeholder || defaultPlaceholder}
    secureTextEntry
    value={value}
    onChangeText={onChange}
  />
);

export default PasswordInput;
