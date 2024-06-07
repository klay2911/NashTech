import { httpClient } from "../httpClient/httpClient";

export const getCategories = (
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
  console.log(queryString);
  return httpClient.get(`/categories?${queryString}`);
};

export const getCategory = (id) => {
  return httpClient.get(`/categories/${id}`);
};

export const createCategory = (category) => {
  return httpClient.post(`/categories`, category);
};

export const updateCategory = (categoryId, updatedData) => {
  return httpClient.put(`/categories/${categoryId}`, updatedData);
};

export const deleteCategory = (categoryId) => {
  return httpClient.delete(`/categories/${categoryId}`);
};
