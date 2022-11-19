import API from './api';
import apiPaths from '../common/apiPaths';

const sendFriendRequest = async (body) => API.post(apiPaths.FRIEND, body);

const friendService = {
  sendFriendRequest,
};

export default friendService;
