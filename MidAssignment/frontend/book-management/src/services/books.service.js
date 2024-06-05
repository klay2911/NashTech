import { httpClient } from '../httpClient/httpClient';

export const getListBooks = (pageNumber = 1, pageSize = 5) =>{
  const params = {
    pageNumber,
    pageSize,
    searchTerm: '',
  };
  const queryString = new URLSearchParams(params).toString();
  return httpClient.get(`/book?${queryString}`);
}

export const getBook = (id) =>{
  return httpClient.get(`/book/${id}`);
}

export const createBook = (bookData) => {
  return httpClient.post(`/book`, bookData, {
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  });
}

export const updateBook = (Id, updatedData) => {
  return httpClient.put(`/book/${Id}`, updatedData,{
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  });
}

export const deleteBook = (bookId) =>{
  console.log('bookId', bookId);
  return httpClient.delete(`/book/${bookId}`);
}

