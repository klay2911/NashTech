import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { validEmail } from "../contants/Regex";
import { useAuthContext } from "../context/AuthContext";
import { apiGetToken } from "../services/profile.service";

const Login = () => {
  const [formValues, setFormValues] = useState({ email: "", password: "" });
  const [formErrors, setFormErrors] = useState("");
  const { setIsAuthenticated } = useAuthContext();
  const Navigate = useNavigate();
  const [clicked, setClicked] = useState({
    email: false,
    password: false,
  });
  const validateForm = (values) => {
    const errors = {};

    if (!values.email) {
      errors.email = "Username is required";
    } else if (!validEmail.test(values.email)) {
      errors.email = "Username is invalid";
    }

    if (!values.password) {
      errors.password = "Password is required";
    } else if (values.password.length < 8) {
      errors.password = "Password must be at least 8 characters";
    }

    return errors;
  };
  const handleClick = (e) => {
    const { name } = e.target;
    setClicked({
      ...clicked,
      [name]: true,
    });
  };

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormValues({ ...formValues, [name]: value });
  };

  useEffect(() => {
    setFormErrors(validateForm(formValues));
  }, [formValues]);

  const handleSubmit = async (event) => {
    event.preventDefault();
    if (Object.keys(formErrors).length === 0) {
      try {
        const response = await apiGetToken(formValues);
        if (response?.data?.token) {
          localStorage.setItem("token", response.data.token);
          setIsAuthenticated(true);
          Navigate("/");
        } else {
          throw new Error("Invalid token");
        }
      } catch (error) {
        console.error("Error logging in:", error);
        setFormErrors({
          email: "Invalid username or password",
          password: "Invalid username or password",
        });
      }
    }
  };

  return (
    <div className="flex min-h-fit w-[500px] items-start justify-center bg-gray-50 px-4 py-12 sm:px-6 lg:px-8">
      <div className="w-full max-w-md space-y-8 pt-12">
        <h2 className="mt-6 text-center text-3xl font-extrabold text-gray-900">
          Login
        </h2>
        <form className="mt-8 space-y-6" onSubmit={handleSubmit}>
          <div>
            <label htmlFor="email" className="sr-only">
              Username:
            </label>
            <input
              type="text"
              name="email"
              value={formValues.email}
              onClick={handleClick}
              onChange={handleChange}
              className="relative block w-full appearance-none rounded-none rounded-t-md border border-gray-300 px-3 py-2 text-gray-900 placeholder-gray-500 focus:z-10 focus:border-indigo-500 focus:outline-none focus:ring-indigo-500 sm:text-sm"
              placeholder="Username"
            />
            {formErrors.email && clicked.email && (
              <label style={{ color: "red" }}> {formErrors.email}</label>
            )}
          </div>
          <div>
            <label htmlFor="password" className="sr-only">
              Password:
            </label>
            <input
              type="password"
              name="password"
              value={formValues.password}
              onClick={handleClick}
              onChange={handleChange}
              className="relative block w-full appearance-none rounded-none rounded-b-md border border-gray-300 px-3 py-2 text-gray-900 placeholder-gray-500 focus:z-10 focus:border-indigo-500 focus:outline-none focus:ring-indigo-500 sm:text-sm"
              placeholder="Password"
            />
            {formErrors.password && clicked.password && (
              <label style={{ color: "red" }}> {formErrors.password}</label>
            )}
          </div>
          <button
            className="group relative flex w-full justify-center rounded-md border border-transparent bg-indigo-600 px-4 py-2 text-sm font-medium text-white hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-indigo-500 focus:ring-offset-2"
            disabled={Object.keys(formErrors).length > 0}
          >
            Login
          </button>
        </form>
      </div>
    </div>
  );
};

export default Login;
