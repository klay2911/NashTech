import { httpClient } from "../httpClient/httpClient";

export const apiGetToken = (userData) => {
  console.log(userData);
  return httpClient.post(`/user/Login`, userData);
};

export const apiGetProfile = (userData) => {
  console.log(userData);
  return httpClient.post(`/user/`, userData);
};
