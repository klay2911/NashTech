import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { apiGetToken } from '../services/profile.service';
import { validEmail } from '../contants/Regex';
import { useAuthContext } from '../context/AuthContext';

const Login = () => {
    const [formValues, setFormValues] = useState({ username: '', password: '' });
    const [formErrors, setFormErrors] = useState({});
    const { setIsAuthenticated} = useAuthContext();
    const Navigate = useNavigate();
    const [clicked, setClicked] = useState({
        email: false,
        password: false
    });
    // const login = () => {
        //call api
        //set localstorage
        //set isAuthenticated true
        //navigate to posts
    // }    
    const validateForm = (values) => {
        const errors = {};

        if (!values.username) {
            errors.username = 'Username is required';
        } else if (!validEmail.test(values.username)) {
            errors.username = 'Username is invalid';
        }

        if (!values.password) {
            errors.password = 'Password is required';
        } else if (values.password.length < 8) {
            errors.password = 'Password must be at least 8 characters';
        }

        return errors;
    }
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
    }

    useEffect(() => {
        setFormErrors(validateForm(formValues));
    }, [formValues]);

    const handleSubmit = async (e) => {
        e.preventDefault();
        if (Object.keys(formErrors).length === 0) {
            try {
                const response = await apiGetToken(formValues.username, formValues.password);
                localStorage.setItem('token', response?.data?.token);
                localStorage.setItem('userId', response?.data?.userId);
                setIsAuthenticated(true);
                Navigate("/");
            } catch (error) {
                console.error("Error logging in:", error);
            }
        }
    };

    return (
        <div className="min-h-fit w-[500px] flex items-start justify-center bg-gray-50 py-12 px-4 sm:px-6 lg:px-8">
        <div className="max-w-md w-full space-y-8 pt-12">
            <h2 className="mt-6 text-center text-3xl font-extrabold text-gray-900">Login</h2>
                <form className="mt-8 space-y-6" onSubmit={handleSubmit}>
                    <div>
                        <label htmlFor="username" className="sr-only">Username:</label>
                        <input
                            type="text"
                            name="username"
                            value={formValues.username}
                            onClick={handleClick}
                            onChange={handleChange}
                            className="appearance-none rounded-none relative block w-full px-3 py-2 border border-gray-300 placeholder-gray-500 text-gray-900 rounded-t-md focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 focus:z-10 sm:text-sm"
                            placeholder="Username"
                        />
                        {formErrors.username && clicked.username && (
                        <label style={{ color: "red" }}> {formErrors.username}</label>
                        )}
                    </div>
                    <div>
                        <label htmlFor="password" className="sr-only">Password:</label>
                        <input
                            type="password"
                            name="password"
                            value={formValues.password}
                            onClick={handleClick}
                            onChange={handleChange}
                            className="appearance-none rounded-none relative block w-full px-3 py-2 border border-gray-300 placeholder-gray-500 text-gray-900 rounded-b-md focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 focus:z-10 sm:text-sm"
                            placeholder="Password"
                        />
                        {formErrors.password && clicked.password && (
                        <label style={{ color: "red" }}> {formErrors.password}</label>
                        )}
                    </div>
                    <button type="submit" className="group relative w-full flex justify-center py-2 px-4 border border-transparent text-sm font-medium rounded-md text-white bg-indigo-600 hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500" disabled={Object.keys(formErrors).length > 0}>Login</button>
                </form>
            </div>
        </div>
    );
};

export default Login;
