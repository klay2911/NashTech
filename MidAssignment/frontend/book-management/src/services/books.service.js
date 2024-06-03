import { httpClient } from '../httpClient/httpClient';

export const getListBooks = (pageNumber = 1, pageSize = 5) =>{
  //const params = `?page=1&sort=name&sortasc&search=123`
  // const params ={
  //   page:1,
  //   sort:'name',
  //   sortType:'asc',
  //   status:['invalid', 'success']
  // }
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
// export const apiGetPosts = (id) =>
//   new Promise(async (resolve, reject) => {
//     try {
//       const response = await instance({
//         method: "get",
//         url: `http://localhost:3000/posts/${id}`,
//       });
//       resolve(response);
//     } catch (error) {
//       reject(error);
//     }
//   });
