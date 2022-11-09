import { StyleSheet } from 'react-native';

const styles = StyleSheet.create({
  screen: {
    backgroundColor: '#260E63',
    width: '100%',
    height: '100%',
  },
  navigationButtonContainer: {
    marginTop: 'auto',
    height: '5%',
    position: 'relative',
  },
  leftButton: { position: 'absolute', width: 100, left: 10 },
  rightButton: { position: 'absolute', width: 100, right: 10 },
  searchBar: { height: 50 },
});

export default styles;
