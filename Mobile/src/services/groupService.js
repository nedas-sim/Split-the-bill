import API from './api';
import apiPaths from '../common/apiPaths';

const getGroups = async (page) => {
  return await API.get(apiPaths.GROUP, { page });
};

const groupService = {
  getGroups,
};

export default groupService;
