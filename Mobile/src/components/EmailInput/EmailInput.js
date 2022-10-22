import React from 'react';
import CoreInput from '../CoreInput/CoreInput';

const EmailInput = ({ value, onChange }) => {
  return (
    <CoreInput
      placeholder="Email"
      keyboardType="email-address"
      value={value}
      onChangeText={onChange}
    />
  );
};

export default EmailInput;
