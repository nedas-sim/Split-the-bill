import API from './api';
import apiPaths from '../common/apiPaths';

const getGroups = async (page, search) => API.get(apiPaths.GROUP, { page, search });
const createGroup = async (body) => API.post(apiPaths.GROUP, body);

const groupService = {
  getGroups,
  createGroup,
};

export default groupService;
