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

const authService = {
  register,
  login,
  logout,
};

export default authService;
