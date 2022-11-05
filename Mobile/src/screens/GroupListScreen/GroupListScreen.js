import React, { useEffect, useState } from 'react';
import { SafeAreaView, BackHandler, Alert, ActivityIndicator, View } from 'react-native';
import { useIsFocused } from '@react-navigation/native';
import groupService from '../../services/groupService';
import GroupList from '../../features/groupList/GroupList/GroupList';
import styles from './styles';
import PageNavigationButton from '../../components/PageNavigationButton/PageNavigationButton';
import backHandlerHelper from '../../common/backHandlerHelper';
import ScreenNames from '../../common/screenNames';

const GroupListScreen = ({ navigation }) => {
  const [groups, setGroups] = useState(null);
  const [loading, setLoading] = useState(false);
  const [page, setPage] = useState(1);
  const [pageButtonActive, setPageButtonActive] = useState({ previous: false, next: false });

  const isFocused = useIsFocused();

  useEffect(() => {
    const getGroups = async () => {
      setLoading(true);
      await retrieveGroups();
      setLoading(false);
    };

    if (isFocused) {
      getGroups();
      backHandlerHelper.setExitListener(BackHandler, Alert, 'exitPress');
    } else {
      backHandlerHelper.removeBackHandler(BackHandler, 'exitPress');
    }
  }, [isFocused, page]);

  const retrieveGroups = async () => {
    const response = await groupService.getGroups(page);
    setGroups(response.data.items);
    setPageButtonActive({ previous: response.data.previousPage, next: response.data.nextPage });
  };

  return (
    <SafeAreaView style={styles.screen}>
      {loading ? (
        <ActivityIndicator size="large" />
      ) : (
        <>
          <GroupList groups={groups} navigation={navigation} />
          {groups?.length > 0 && (
            <View style={styles.navigationButtonContainer}>
              <View style={styles.leftButton}>
                <PageNavigationButton
                  enabled={pageButtonActive.previous}
                  text="<"
                  onClick={() => setPage(page - 1)}
                />
              </View>
              <View style={styles.rightButton}>
                <PageNavigationButton
                  enabled={pageButtonActive.next}
                  text=">"
                  onClick={() => setPage(page + 1)}
                />
              </View>
            </View>
          )}
          <View style={styles.newGroupBtnContainer}>
            <PageNavigationButton
              enabled
              text="+"
              onClick={() => navigation.navigate(ScreenNames.createGroupScreen)}
            />
          </View>
        </>
      )}
    </SafeAreaView>
  );
};

export default GroupListScreen;
