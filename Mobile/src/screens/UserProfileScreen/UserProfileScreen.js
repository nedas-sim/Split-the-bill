import React, { useState, useEffect } from 'react';
import { SafeAreaView, View, Button, Alert, ActivityIndicator } from 'react-native';
import styles from './styles';
import CoreInput from '../../components/CoreInput/CoreInput';
import userService from '../../services/userService';

const updateableFields = ['username'];

const UserProfileScreen = () => {
  const [profile, setProfile] = useState(null);
  const [loading, setLoading] = useState(false);

  const showLoadingBar = loading || profile === null;

  useEffect(() => {
    const fetchProfile = async () => {
      try {
        setLoading(true);
        const response = await userService.getProfile();
        setProfile(response.data);
      } catch (ex) {
        Alert.alert('Error', ex.response.data.message);
      } finally {
        setLoading(false);
      }
    };

    fetchProfile();
  }, []);

  const handleUpdateButtonPress = async () => {
    try {
      const updateRequestBody = Object.fromEntries(
        Object.entries(profile).filter(([key]) => updateableFields.includes(key))
      );

      const response = await userService.updateProfile(updateRequestBody);
      Alert.alert('Success', 'Update successful!');
      setProfile(response.data);
    } catch (ex) {
      Alert.alert('Error', ex.response.data.message);
    }
  };

  return (
    <SafeAreaView style={styles.screen}>
      {showLoadingBar ? (
        <ActivityIndicator size="large" />
      ) : (
        <>
          <View style={styles.inputsContainer}>
            {updateableFields.map((fieldName) => (
              <View style={styles.inputWrapper} key={fieldName}>
                <CoreInput
                  placeholder={fieldName}
                  value={profile[fieldName]}
                  onChangeText={(inputValue) =>
                    setProfile((oldProfile) => ({ ...oldProfile, [fieldName]: inputValue }))
                  }
                />
              </View>
            ))}
          </View>
          <Button title="Update" onPress={handleUpdateButtonPress} />
        </>
      )}
    </SafeAreaView>
  );
};

export default UserProfileScreen;
