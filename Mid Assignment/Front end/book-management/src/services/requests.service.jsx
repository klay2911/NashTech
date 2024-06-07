import { httpClient } from "../httpClient/httpClient";

export const getWaitingRequest = (
  pageNumber = 1,
  pageSize = 5,
  searchTerm = "",
) => {
  const params = {
    pageNumber,
    pageSize,
    searchTerm,
  };
  const queryString = new URLSearchParams(params).toString();
  return httpClient.get(`/requests?${queryString}`);
};

export const getRequest = (id) => {
  return httpClient.get(`/requests/${id}`);
};

export const getBookUserRequest = (
  userId,
  pageNumber = 1,
  pageSize = 5,
  searchTerm = "",
) => {
  const params = {
    pageNumber,
    pageSize,
    searchTerm,
  };
  const queryString = new URLSearchParams(params).toString();
  console.log("Query string:", queryString);
  return httpClient.get(`/requests/user/${userId}?${queryString}`);
};

export const createBorrowRequest = async (bookIds) => {
  return httpClient
    .post(`/requests`, JSON.stringify(bookIds))
    .catch((error) => {
      console.error("Error creating borrow request:", error);
    });
};

export const updateRequestStatus = (requestId, updatedStatus) => {
  const params = {
    status: updatedStatus,
  };
  const queryString = new URLSearchParams(params).toString();
  return httpClient.put(`/requests/${requestId}?${queryString}`);
};
