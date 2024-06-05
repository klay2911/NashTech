import {httpClient} from "../httpClient/httpClient";

export const apiGetToken = (userData) => {
  console.log(userData);
  return httpClient.post(`/user/Login`, userData);
}


export const apiGetProfile = (userData) => {
  console.log(userData);
  return httpClient.post(`/user/`, userData);
}

// export const apiGetProfile = (id) =>
//   new Promise(async (resolve, reject) => {
//     try {
//       const response = await httpClient({
//         method: "get",
//         url: `https://60dff0ba6b689e001788c858.mockapi.io/users/${id}`,
//       });
//       resolve(response);
//     } catch (error) {
//       reject(error);
//     }
//   });
