import { Screens } from "./src/common/screens";
import { NavigationContainer } from "@react-navigation/native";
import { createNativeStackNavigator } from "@react-navigation/native-stack";

const Stack = createNativeStackNavigator();

export default function App() {
  return (
    <NavigationContainer>
      <Stack.Navigator>
        {Object.entries(Screens).map(([key, value]) => (
          <Stack.Screen
            key={key}
            name={value.name}
            component={value.component}
          />
        ))}
      </Stack.Navigator>
    </NavigationContainer>
  );
}
