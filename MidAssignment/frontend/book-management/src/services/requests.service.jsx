import { httpClient } from '../httpClient/httpClient';

export const getWaitingRequest = (pageNumber = 1, pageSize = 5) =>{
  const params = {
    pageNumber,
    pageSize,
    searchTerm: '',
  };
  const queryString = new URLSearchParams(params).toString();
  return httpClient.get(`/request?${queryString}`);
}

export const getRequest = (id) =>{
  return httpClient.get(`/request/${id}`);
}

export const getBookUserRequest = (userId, pageNumber = 1, pageSize = 5) =>{
  //console.log(request);
  const params = {
    pageNumber,
    pageSize,
    searchTerm: '',
  };
  const queryString = new URLSearchParams(params).toString();
  return httpClient.get(`/request/user/${userId}?${queryString}`);
}

export const createBorrowRequest = (request) =>{
  console.log(request);
  return httpClient.post(`/request`, request);
}


export const updateRequestStatus = (requestId, updatedStatus) => {

  return httpClient.put(`/request/${requestId}`, updatedStatus);
}


