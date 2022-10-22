import React, { useEffect, useState } from 'react';
import { SafeAreaView, Text, BackHandler, Alert } from 'react-native';

import API from '../services/api';

const GroupListScreen = () => {
  const [id, setId] = useState('');

  useEffect(() => {
    BackHandler.addEventListener('hardwareBackPress', () => {
      Alert.alert(
        'Exit',
        'Do you want to exit the app?',
        [
          {
            text: 'No',
          },
          {
            text: 'Yes',
            onPress: () => BackHandler.exitApp(),
          },
        ],
        {
          cancelable: false,
        }
      );
      return true;
    });
  }, []);

  useEffect(() => {
    const callFunc = async () => {
      const response = await API.get('/test/authPing');
      setId(response.data.id);
    };

    callFunc();
  }, []);

  return (
    <SafeAreaView>
      <Text>{id}</Text>
    </SafeAreaView>
  );
};

export default GroupListScreen;
