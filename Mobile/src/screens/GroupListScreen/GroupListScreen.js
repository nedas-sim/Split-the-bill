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
      const fetchAsync = async () => {
        if (firstRender.current) {
          firstRender.current = false;
          setPage(1);
        }
      };
      fetchAsync();

      // setup event listener on mount
      backHandlerHelper.setExitListener(BackHandler, Alert, 'exitPress');
      return () => {
        // remove event listener on unmount
        backHandlerHelper.removeBackHandler(BackHandler, 'exitPress');
        firstRender.current = true;
      };
    }, [])
  );

  useEffect(() => {
    const getGroups = async () => {
      if (firstRender.current === false) {
        if (firstRender.current) setLoading(true);
        await retrieveGroups();
        if (firstRender.current) setLoading(false);
      }
    };

    getGroups();
    firstRender.current = false;
  }, [page]);

  const retrieveGroups = async () => {
    console.log(page);
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
