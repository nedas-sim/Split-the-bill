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
  const [page, setPage] = useState([1]);
  const [pageButtonActive, setPageButtonActive] = useState({ previous: false, next: false });

  const firstRender = useRef(true);

  useFocusEffect(
    useCallback(() => {
      const fetchAsync = async () => {
        //console.log('focus');
          setPage([1]);

        /*if (firstRender.current) {
          //console.log('focus set page to 1');
          firstRender.current = false;
          setPage({value: 1});
        }*/
      };
      //fetchAsync();
      firstRender.current = false;
      console.log('focus');
      setPage([...page]);
      // setup event listener on mount
      backHandlerHelper.setExitListener(BackHandler, Alert, 'exitPress');
      return () => {
        // remove event listener on unmount
        //console.log('unmount focus');
        backHandlerHelper.removeBackHandler(BackHandler, 'exitPress');
        //firstRender.current = true;
      };
    }, [])
  );

  useEffect(() => {
    const getGroups = async () => {
      //console.log('use effect');
      if (firstRender.current === false) {
        if (firstRender.current) setLoading(true);
        //console.log('use effect retrieve');
        await retrieveGroups();
        if (firstRender.current) setLoading(false);
      }
    };

    getGroups();
    firstRender.current = false;
    return () => {
      //console.log('unmount use effect');
      firstRender.current = true;
    }
  }, [page]);

  const retrieveGroups = async () => {
    console.log('retrieve');
    const response = await groupService.getGroups(page[0]);
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
                  onClick={() => setPage([page[0] - 1])}
                />
              </View>
              <View style={styles.rightButton}>
                <PageNavigationButton
                  enabled={pageButtonActive.next}
                  text=">"
                  onClick={() => setPage([page[0] + 1])}
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
