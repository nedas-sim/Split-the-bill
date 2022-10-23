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
  newGroupBtnContainer: {
    width: 100,
    height: 50,
    position: 'absolute',
    bottom: 0,
    left: '50%',
    marginLeft: -50, // marginLeft = -0.5 * width, then it's centered
  },
});

export default styles;
