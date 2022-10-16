import API from "./api";
import apiPaths from "../common/apiPaths";

const register = async (body) => {
  await API.post(apiPaths.REGISTER, body);
};

const login = async (body) => {
  await API.post(apiPaths.LOGIN, body);
};

const logout = async () => {
  await API.post(apiPaths.LOGOUT, {});
};

const isLoggedIn = async () => {
  await API.post(apiPaths.IS_LOGGED_IN, {});
};

const authService = {
  register,
  login,
  logout,
  isLoggedIn,
};

export default authService;
