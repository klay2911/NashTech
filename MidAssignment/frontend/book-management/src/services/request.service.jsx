import { httpClient } from '../httpClient/httpClient';

export const getWaitingRequest = (pageNumber = 1, pageSize = 5) =>{
  const params = {
    pageNumber,
    pageSize,
    searchTerm: '',
  };
   return httpClient.get(`/category?${queryString}`);
}

export const getRequest = (id) =>{
  return httpClient.get(`/category/${id}`);
}

export const createCategory = (category) =>{
  return httpClient.post(`/category`, category);
}

export const updateRequestStatus = (requestId, updatedData) => {
  return httpClient.put(`/category/${requestId}`, updatedData);
}

export const deleteCategory = (categoryId) =>{
  return httpClient.delete(`/category/${categoryId}`);
}
