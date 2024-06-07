import { httpClient } from "../httpClient/httpClient";

export const getListBooks = (pageNumber = 1, pageSize = 5, searchTerm = "") => {
  const params = {
    pageNumber,
    pageSize,
    searchTerm,
  };
  const queryString = new URLSearchParams(params).toString();
  return httpClient.get(`/books?${queryString}`);
};

export const getBook = (id) => {
  return httpClient.get(`/books/${id}`);
};

export const createBook = (bookData) => {
  return httpClient.post(`/books`, bookData, {
    headers: {
      "Content-Type": "multipart/form-data",
    },
  });
};

export const updateBook = (Id, updatedData) => {
  return httpClient.put(`/books/${Id}`, updatedData, {
    headers: {
      "Content-Type": "multipart/form-data",
    },
  });
};

export const deleteBook = (bookId) => {
  return httpClient.delete(`/books/${bookId}`);
};
