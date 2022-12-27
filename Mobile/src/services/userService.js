import API from './api';
import apiPaths from '../common/apiPaths';

const getUsers = async (params) => API.get(apiPaths.USER, params);
const getProfile = async () => API.get(apiPaths.USER_PROFILE);
const updateProfile = async (body) => API.put(apiPaths.USER_PROFILE, body);

const userService = { getUsers, getProfile, updateProfile };

export default userService;
