import API from './api';
import apiPaths from '../common/apiPaths';

const sendFriendRequest = async (body) => API.post(apiPaths.FRIEND, body);
const getFriendRequests = async (params) => API.get(apiPaths.FRIEND_REQUEST, params);
const interactWithFriendRequest = async (body) => API.put(apiPaths.FRIEND_REQUEST, body);
const getFriends = async (params) => API.get(apiPaths.FRIEND, params);
const getGroupSuggestions = async (params) => API.get(apiPaths.GROUPS_FOR_FRIEND, params);

const friendService = {
  sendFriendRequest,
  getFriendRequests,
  interactWithFriendRequest,
  getFriends,
  getGroupSuggestions,
};

export default friendService;
