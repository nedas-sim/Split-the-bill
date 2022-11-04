import React from 'react';
import CoreInput from '../CoreInput/CoreInput';

const defaultPlaceholder = 'Password';

function PasswordInput({ value, onChange, placeholder }) {
  return (
    <CoreInput
      placeholder={placeholder || defaultPlaceholder}
      secureTextEntry
      value={value}
      onChangeText={onChange}
    />
  );
}

export default PasswordInput;
