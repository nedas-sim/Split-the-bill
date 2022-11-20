import API from './api';
import apiPaths from '../common/apiPaths';

const sendFriendRequest = async (body) => API.post(apiPaths.FRIEND, body);
const getFriendRequests = async (page, search) =>
  API.get(apiPaths.FRIEND_REQUEST, { page, search });

const friendService = {
  sendFriendRequest,
  getFriendRequests,
};

export default friendService;
