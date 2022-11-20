import React, { useState, useEffect } from 'react';
import { SafeAreaView, ActivityIndicator, View } from 'react-native';
import { useIsFocused } from '@react-navigation/native';
import { RefetchContext } from '../../../common/context';
import styles from './styles';
import CoreInput from '../../../components/CoreInput/CoreInput';
import GenericList from '../GenericList/GenericList';
import PageNavigationButton from '../../../components/PageNavigationButton/PageNavigationButton';

const GenericListScreen = (props) => {
  const { searchEnabled, fetchItems, renderItem, noItemsMessages, onAddNew, emptySearch } = props;

  const [items, setItems] = useState(null);
  const [loading, setLoading] = useState(false);
  const [page, setPage] = useState(1);
  const [pageButtonActive, setPageButtonActive] = useState({ previous: false, next: false });
  const [search, setSearch] = useState('');

  const isFocused = useIsFocused();

  useEffect(() => {
    if (searchEnabled && !emptySearch && !(search?.length >= 3)) {
      return;
    }

    const debounceTimeInMs = emptySearch && search.length === 0 ? 0 : 1000;

    const debounceTimeout = setTimeout(() => {
      const getItems = async () => {
        setLoading(true);
        await retrieveItems();
        setLoading(false);
      };

      if (isFocused) {
        getItems();
      }
    }, debounceTimeInMs);

    return () => clearTimeout(debounceTimeout);
  }, [isFocused, page, search]);

  const retrieveItems = async () => {
    const response = searchEnabled ? await fetchItems(page, search) : await fetchItems(page);
    setItems(response.data.items);
    setPageButtonActive({ previous: response.data.previousPage, next: response.data.nextPage });
  };

  return (
    <SafeAreaView style={styles.screen}>
      {searchEnabled && (
        <View style={styles.searchBar}>
          <CoreInput
            value={search}
            onChangeText={(term) => setSearch(term)}
            placeholder="Search..."
          />
        </View>
      )}
      {loading ? (
        <ActivityIndicator size="large" />
      ) : (
        <>
          <RefetchContext.Provider value={retrieveItems}>
            <GenericList items={items} renderItem={renderItem} noItemsMessages={noItemsMessages} />
          </RefetchContext.Provider>
          {items?.length > 0 && (
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
          {onAddNew && (
            <View style={styles.addNewButtonContainer}>
              <PageNavigationButton enabled text="+" onClick={onAddNew} />
            </View>
          )}
        </>
      )}
    </SafeAreaView>
  );
};

export default GenericListScreen;
