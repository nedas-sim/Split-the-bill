import API from './api';
import apiPaths from '../common/apiPaths';

const sendFriendRequest = async (body) => API.post(apiPaths.FRIEND, body);
const getFriendRequests = async (page, search) =>
  API.get(apiPaths.FRIEND_REQUEST, { page, search });
const interactWithFriendRequest = async (body) => API.put(apiPaths.FRIEND_REQUEST, body);

const friendService = {
  sendFriendRequest,
  getFriendRequests,
  interactWithFriendRequest,
};

export default friendService;
