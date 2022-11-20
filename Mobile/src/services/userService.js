import API from './api';
import apiPaths from '../common/apiPaths';

const getUsers = async (page, search) => API.get(apiPaths.USER, { page, search });
const getProfile = async () => API.get(apiPaths.USER_PROFILE);
const updateProfile = async (body) => API.put(apiPaths.USER_PROFILE, body);

const userService = { getUsers, getProfile, updateProfile };

export default userService;
