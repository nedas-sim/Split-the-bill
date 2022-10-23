import React, { useEffect, useState, useRef } from 'react';
import { SafeAreaView, BackHandler, Alert, ActivityIndicator, View } from 'react-native';
import groupService from '../../services/groupService';
import GroupList from '../../features/groupList/GroupList/GroupList';
import styles from './styles';
import PageNavigationButton from '../../components/PageNavigationButton/PageNavigationButton';
import setupBackHandler from '../../common/backHandlerHelper';

const GroupListScreen = () => {
  const [groups, setGroups] = useState(null);
  const [loading, setLoading] = useState(false);
  const [page, setPage] = useState(1);
  const [pageButtonActive, setPageButtonActive] = useState({ previous: false, next: false });

  const firstRender = useRef(true);

  useEffect(() => {
    setupBackHandler(BackHandler, Alert);
  }, []);

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
              <View style={styles.buttonWrapper}>
                <PageNavigationButton
                  enabled={pageButtonActive.previous}
                  text="<"
                  onClick={() => setPage((curr) => curr - 1)}
                />
              </View>
              <View style={styles.buttonWrapper}>
                <PageNavigationButton
                  enabled={pageButtonActive.next}
                  text=">"
                  onClick={() => setPage((curr) => curr + 1)}
                />
              </View>
            </View>
          )}
        </>
      )}
    </SafeAreaView>
  );
};

export default GroupListScreen;
