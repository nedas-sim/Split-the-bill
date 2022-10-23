import API from './api';
import apiPaths from '../common/apiPaths';

const getGroups = async (page) => {
  return await API.get(apiPaths.GROUP, { page });
};

const createGroup = async (body) => {
  return await API.post(apiPaths.GROUP, body);
};

const groupService = {
  getGroups,
  createGroup,
};

export default groupService;
