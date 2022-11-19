import React, { useState, useEffect } from 'react';
import { SafeAreaView, ActivityIndicator, View } from 'react-native';
import { useIsFocused } from '@react-navigation/native';
import { RefetchContext } from '../../common/context';
import styles from './styles';
import userService from '../../services/userService';
import PageNavigationButton from '../../components/PageNavigationButton/PageNavigationButton';
import UserList from '../../features/userList/UserList/UserList';
import CoreInput from '../../components/CoreInput/CoreInput';

const UserListScreen = () => {
  const [users, setUsers] = useState(null);
  const [loading, setLoading] = useState(false);
  const [page, setPage] = useState(1);
  const [pageButtonActive, setPageButtonActive] = useState({ previous: false, next: false });
  const [search, setSearch] = useState('');

  const isFocused = useIsFocused();

  useEffect(() => {
    if (!(search?.length >= 3)) {
      return;
    }

    const timeout = setTimeout(() => {
      const getUsers = async () => {
        setLoading(true);
        await retrieveUsers();
        setLoading(false);
      };

      getUsers();
    }, 1000);

    return () => clearTimeout(timeout);
  }, [isFocused, page, search]);

  const retrieveUsers = async () => {
    const response = await userService.getUsers(page, search);
    setUsers(response.data.items);
    setPageButtonActive({ previous: response.data.previousPage, next: response.data.nextPage });
  };

  const refetchContextValue = {
    fetch: retrieveUsers,
  };

  return (
    <SafeAreaView style={styles.screen}>
      <View style={styles.searchBar}>
        <CoreInput
          value={search}
          onChangeText={(term) => setSearch(term)}
          placeholder="Search..."
        />
      </View>
      {loading ? (
        <ActivityIndicator size="large" />
      ) : (
        <>
          <RefetchContext.Provider value={refetchContextValue}>
            <UserList users={users} />
          </RefetchContext.Provider>
          {users?.length > 0 && (
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
        </>
      )}
    </SafeAreaView>
  );
};

export default UserListScreen;
