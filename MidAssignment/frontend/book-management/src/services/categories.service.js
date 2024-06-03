import { httpClient } from '../httpClient/httpClient';

export const getCategories = (pageNumber = 1, pageSize = 5) =>{
  const params = {
    pageNumber,
    pageSize,
    searchTerm: '',
  };
  const queryString = new URLSearchParams(params).toString();

   return httpClient.get(`/category?${queryString}`);
}

export const getCategory = (id) =>{
  return httpClient.get(`/category/${id}`);
}

export const createCategory = (category) =>{
  return httpClient.post(`/category`, category);
}

export const updateCategory = (categoryId, updatedData) => {
  return httpClient.put(`/category/${categoryId}`, updatedData);
}

export const deleteCategory = (categoryId) =>{
  return httpClient.delete(`/category/${categoryId}`);
}
