import API from './api';
import apiPaths from '../common/apiPaths';

const getGroups = async (params) => API.get(apiPaths.GROUP, params);
const createGroup = async (body) => API.post(apiPaths.GROUP, body);
const getFriendSuggestions = async (params) => API.get(apiPaths.FRIENDS_FOR_GROUP, params);
const sendInvitationToGroup = async (body) => API.post(apiPaths.GROUP_INVITE, body);

const groupService = {
  getGroups,
  createGroup,
  getFriendSuggestions,
  sendInvitationToGroup,
};

export default groupService;
