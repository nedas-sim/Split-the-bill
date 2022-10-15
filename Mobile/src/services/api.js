import axios from "axios";
import apiPaths from "../common/apiPaths";

const axiosInstance = axios.create({
  baseURL: apiPaths.BASE_URL,
  headers: {
    Accept: "application/json",
    "Content-Type": "application/json",
  },
});

const API = {
  get(path) {
    return axiosInstance.get(path);
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
