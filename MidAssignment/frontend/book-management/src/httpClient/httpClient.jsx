import axios from "axios";

const instance = axios.create({
    baseURL: "https://localhost:44352/api/",
    headers: {
      "Content-Type": "application/json",
    },
  });
  // const instance = axios.create({
  //   baseURL: "https://jsonplaceholder.typicode.com",
  //   headers: {
  //     "Content-Type": "application/json",
  //   },
  // },);
  
  instance.interceptors.request.use(
    function (config) {
      const token = localStorage.getItem("token");
      if (token) {
        config.headers.Authorization = `Bearer ${token}`;
      }
      return config;
    },
    function (error) {
      return Promise.reject(error);
    }
  );
  
  // instance.interceptors.request.use(
  //   function (response) {
  //     return response;
  //   },
  //   function (error) {
  //     return Promise.reject(error);
  //   }
  // );
  instance.interceptors.response.use(
    function (res) {
      return {data: res?.data, status: res.data.status};
    },
    async (err) => {
      if (err.response.status === 401) {
        window.location.href = "/login";
        localStorage.removeItem("token");
        return Promise.reject(err.response.data);
      }
      //case 403 permission denied 
      //you dont have persmission to access this resource
      return Promise.reject(err);
    }
  );
  export const httpClient = instance;