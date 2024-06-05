import { httpClient } from "../httpClient/httpClient";

export const getWaitingRequest = (pageNumber = 1, pageSize = 5) => {
  const params = {
    pageNumber,
    pageSize,
    searchTerm: "",
  };
  const queryString = new URLSearchParams(params).toString();
  return httpClient.get(`/request?${queryString}`);
};

export const getRequest = (id) => {
  return httpClient.get(`/request/${id}`);
};

export const getBookUserRequest = (userId, pageNumber = 1, pageSize = 5) => {
  const params = {
    pageNumber,
    pageSize,
    searchTerm: "",
  };
  const queryString = new URLSearchParams(params).toString();
  return httpClient.get(`/request/user/${userId}?${queryString}`);
};

export const createBorrowRequest = async (bookIds) => {
  return httpClient.post(`/request`, JSON.stringify(bookIds)).catch((error) => {
    console.error("Error creating borrow request:", error);
  });
};

export const updateRequestStatus = (requestId, updatedStatus) => {
  const params = {
    status: updatedStatus,
  };
  const queryString = new URLSearchParams(params).toString();
  return httpClient.put(`/request/${requestId}?${queryString}`);
};
