import { NavigationContainer } from '@react-navigation/native';
import { createNativeStackNavigator } from '@react-navigation/native-stack';
import Screens from './src/common/screens';

const Stack = createNativeStackNavigator();

const App = () => (
  <NavigationContainer>
    <Stack.Navigator>
      {Object.entries(Screens).map(([key, value]) => (
        <Stack.Screen
          key={key}
          name={value.name}
          component={value.component}
          options={value.options}
        />
      ))}
    </Stack.Navigator>
  </NavigationContainer>
);

export default App;
