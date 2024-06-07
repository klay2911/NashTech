import axios from "axios";

const instance = axios.create({
  baseURL: "https://localhost:44352/api/",
  headers: {
    "Content-Type": "application/json",
  },
});

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
  },
);

instance.interceptors.response.use(
  function (res) {
    return { data: res?.data, status: res.data.status };
  },
  async (err) => {
    try {
      if (err.response.status >= 400) {
        if (err.response.status === 401) {
          window.location.href = "/login";
          alert("You are not authorized to access this resource");
          localStorage.removeItem("token");
          return Promise.reject(err.response.data);
        } else if (err.response.status === 403) {
          alert("You don't have permission to access this resource");
          return Promise.reject(err.response.data);
        } else if (err.response.status === 404) {
          alert("Resource not found");
          return Promise.reject(err.response.data);
        } 
        else {
          alert(
            "An error occured while processing your request. Please try again later.",
          );
        }
      }
    } catch (error) {
      console.error(error);
      alert(
        "An error occured while processing your request. Please try again later.",
      );
    }
  },
);
export const httpClient = instance;
