import API from './api';
import apiPaths from '../common/apiPaths';

const getGroups = async (page) => API.get(apiPaths.GROUP, { page });

const createGroup = async (body) => API.post(apiPaths.GROUP, body);

const groupService = {
  getGroups,
  createGroup,
};

export default groupService;
