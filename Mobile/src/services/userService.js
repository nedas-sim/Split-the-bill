import API from './api';
import apiPaths from '../common/apiPaths';

const getUsers = async (page, search) => API.get(apiPaths.USER, { page, search });

const userService = { getUsers };

export default userService;
