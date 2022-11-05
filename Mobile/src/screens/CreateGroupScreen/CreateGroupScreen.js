import React, { useState, useCallback } from 'react';
import { View, Button, SafeAreaView, Alert, BackHandler } from 'react-native';
import { useFocusEffect } from '@react-navigation/native';
import styles from './styles';
import CoreInput from '../../components/CoreInput/CoreInput';
import groupService from '../../services/groupService';
import ScreenNames from '../../common/screenNames';
import backHandlerHelper from '../../common/backHandlerHelper';

const CreateGroupScreen = ({ navigation }) => {
  const [groupInfo, setGroupInfo] = useState({
    name: '',
  });

  useFocusEffect(
    useCallback(() => {
      // setup event listener on mount
      backHandlerHelper.setBackScreen(
        BackHandler,
        navigation,
        ScreenNames.groupList,
        'backToGroupList'
      );
      return () => {
        // remove event listener on unmount
        backHandlerHelper.removeBackHandler(BackHandler, 'backToGroupList');
      };
    }, [])
  );

  const handleCreateGroup = async () => {
    try {
      const response = await groupService.createGroup(groupInfo);
      Alert.alert('Success', 'Group created successfully!', [
        {
          text: 'Go to Groups List',
          onPress: () => navigation.navigate(ScreenNames.groupList),
        },
        {
          text: 'Go to created groups details',
          onPress: () =>
            navigation.navigate(ScreenNames.groupDetailsScreen, {
              groupId: response.data.id,
            }),
        },
      ]);
    } catch (ex) {
      Alert.alert('Error', ex.response.data.message);
    }
  };

  return (
    <SafeAreaView style={styles.screen}>
      <View style={styles.inputContainer}>
        <CoreInput
          placeholder="Group name"
          value={groupInfo.name}
          onChangeText={(name) => setGroupInfo((info) => ({ ...info, name }))}
        />
      </View>
      <View style={styles.buttonContainer}>
        <Button title="Create" onPress={handleCreateGroup} />
      </View>
    </SafeAreaView>
  );
};

export default CreateGroupScreen;
