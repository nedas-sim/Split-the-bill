import React, { useEffect, useState, useRef, useCallback } from 'react';
import { SafeAreaView, BackHandler, Alert, ActivityIndicator, View } from 'react-native';
import groupService from '../../services/groupService';
import GroupList from '../../features/groupList/GroupList/GroupList';
import styles from './styles';
import PageNavigationButton from '../../components/PageNavigationButton/PageNavigationButton';
import backHandlerHelper from '../../common/backHandlerHelper';
import ScreenNames from '../../common/screenNames';
import { useFocusEffect } from '@react-navigation/native';

const GroupListScreen = ({ navigation }) => {
  const [groups, setGroups] = useState(null);
  const [loading, setLoading] = useState(false);
  const [page, setPage] = useState(1);
  const [pageButtonActive, setPageButtonActive] = useState({ previous: false, next: false });

  const firstRender = useRef(true);

  useFocusEffect(
    useCallback(() => {
      // setup event listener on mount
      backHandlerHelper.setupBackHandler(BackHandler, Alert);
      return () => {
        // remove event listener on unmount
        backHandlerHelper.removeBackHandler(BackHandler);
      };
    }, [])
  );

  useEffect(() => {
    const retrieveGroups = async () => {
      if (firstRender.current) setLoading(true);
      const response = await groupService.getGroups(page);
      setGroups(response.data.items);
      setPageButtonActive({ previous: response.data.previousPage, next: response.data.nextPage });
      if (firstRender.current) setLoading(false);

      firstRender.current = false;
    };

    retrieveGroups();
  }, [page]);

  return (
    <SafeAreaView style={styles.screen}>
      {loading ? (
        <ActivityIndicator size="large" />
      ) : (
        <>
          <GroupList groups={groups} />
          {groups?.length > 0 && (
            <View style={styles.navigationButtonContainer}>
              <View style={styles.leftButton}>
                <PageNavigationButton
                  enabled={pageButtonActive.previous}
                  text="<"
                  onClick={() => setPage((curr) => curr - 1)}
                />
              </View>
              <View style={styles.rightButton}>
                <PageNavigationButton
                  enabled={pageButtonActive.next}
                  text=">"
                  onClick={() => setPage((curr) => curr + 1)}
                />
              </View>
            </View>
          )}
          <View style={styles.newGroupBtnContainer}>
            <PageNavigationButton
              enabled={true}
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
