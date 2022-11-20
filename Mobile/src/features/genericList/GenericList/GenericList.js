import React from 'react';
import { ScrollView, SafeAreaView } from 'react-native';
import styles from './styles';
import StaticTextArea from '../../../components/StaticTextArea/StaticTextArea';

const GenericList = ({ items, renderItem, noItemsMessages }) => (
  <SafeAreaView style={styles.container}>
    {items?.length > 0 ? (
      <ScrollView style={styles.listContainer}>{items.map((item) => renderItem(item))}</ScrollView>
    ) : (
      <StaticTextArea texts={noItemsMessages} />
    )}
  </SafeAreaView>
);

export default GenericList;
