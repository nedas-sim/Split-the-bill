import axios from 'axios';
// eslint-disable-next-line import/no-unresolved
import { REACT_APP_API_BASE_URL } from '@env';

const axiosInstance = axios.create({
  baseURL: REACT_APP_API_BASE_URL,
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
