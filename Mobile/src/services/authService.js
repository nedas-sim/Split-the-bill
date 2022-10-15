import API from "./api";
import apiPaths from "../common/apiPaths";

const register = async (body) => {
  await API.post(apiPaths.REGISTER, body);
};

const authService = {
  register,
};

export default authService;
