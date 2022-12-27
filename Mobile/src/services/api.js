import axios from 'axios';
import config from '../common/config';

const axiosInstance = axios.create({
  baseURL: config.BASE_URL,
  headers: {
    Accept: 'application/json',
    'Content-Type': 'application/json',
  },
});

const API = {
  get(path, params) {
    return axiosInstance.get(path, { params });
  },
  post(path, body) {
    return axiosInstance.post(path, body);
  },
  put(path, body) {
    return axiosInstance.put(path, body);
  },
  delete(path) {
    return axiosInstance.delete(path);
  },
};

export default API;
