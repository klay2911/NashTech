import React, {useEffect, useState } from 'react';
import './Register.css';

const RegisterForm = () => {
    const [values, setValues] = useState({
        username: '',
        email: '',
        gender: '',
        password: '',
        retypePassword: '',
        agreement: false,
    });

    const [errors, setErrors] = useState({});
    const [isFormValid, setIsFormValid] = useState(false);

    // useEffect(() => {
    //     validateForm();
    // }, [values]);

    // const handleFieldChange = (event) => {
    //     setValues({
    //         ...values,
    //         [event.target.name]: event.target.value
    //     });
    // };
    const handleFieldChange = (event) => {
        setValues({
            ...values,
            [event.target.name]: event.target.value
        });
    
        // Validate the field that just lost focus
        validateField(event.target.name, event.target.value);

        setIsFormValid(Object.keys(errors).length === 0 && Object.values(values).every(value => value));

    };
    const validateField = (name, value) => {
        let errors = {};
    
        const usernameRegex = /^[A-Za-z0-9]{4,}$/;
        const emailRegex = /\S+@\S+\.\S+/;
        const passwordRegex = /.{8,}/;
    
        if (name === 'username') {
            if (!value) {
                errors.username = 'Username is required';
            } else if (!usernameRegex.test(value)) {
                errors.username = 'Username must be at least 4 characters and contain only A-Z, a-z, 0-9';
            }
        }
    
        if (name === 'email') {
            if (!value) {
                errors.email = 'Email is required';
            } else if (!emailRegex.test(value)) {
                errors.email = 'Email is not valid';
            }
        }
    
        if (name === 'password') {
            if (!value) {
                errors.password = 'Password is required';
            } else if (!passwordRegex.test(value)) {
                errors.password = 'Password must be at least 8 characters';
            }
        }
    
        if (name === 'retypePassword') {
            if (!value) {
                errors.retypePassword = 'Retype password is required';
            } else if (value !== values.password) {
                errors.retypePassword = 'Retype password must be equal to password';
            }
        }
    
        if (name === 'agreement') {
            if (!value) {
                errors.agreement = 'You must agree to the terms';
            }
        }
    
        setErrors(errors);
    };

    const validateForm = () => {
        let errors = {};
    
        const usernameRegex = /^[A-Za-z0-9]{4,}$/;
        const emailRegex = /\S+@\S+\.\S+/;
        const passwordRegex = /.{8,}/;
    
        if (!values.username) {
            errors.username = 'Username is required';
        } else if (!usernameRegex.test(values.username)) {
            errors.username = 'Username must be at least 4 characters and contain only A-Z, a-z, 0-9';
        }
    
        if (!values.email) {
            errors.email = 'Email is required';
        } else if (!emailRegex.test(values.email)) {
            errors.email = 'Email is not valid';
        }
    
        if (!values.password) {
            errors.password = 'Password is required';
        } else if (!passwordRegex.test(values.password)) {
            errors.password = 'Password must be at least 8 characters';
        }
    
        if (!values.retypePassword) {
            errors.retypePassword = 'Retype password is required';
        } else if (values.retypePassword !== values.password) {
            errors.retypePassword = 'Retype password must be equal to password';
        }
    
        if (!values.agreement) {
            errors.agreement = 'You must agree to the terms';
        }
    
        setErrors(errors);
        setIsFormValid(Object.keys(errors).length === 0);
    };

    const handleSubmit = (e) => {
        e.preventDefault(); 
        //validateForm();
    if (isFormValid) {
            console.log('Username:', values.username);
            console.log('Email:', values.email);
            console.log('Gender:', values.gender);
            console.log('Password:', values.password);
            console.log('Retype Password:', values.retypePassword);
            console.log('Agreement:', values.agreement);
            setValues({
                username: '',
                email: '',
                gender: '',
                password: '',
                retypePassword: '',
                agreement: false,
            });
        }
    };

    return (
        <form onSubmit={handleSubmit}>
            <div>
                <label>Username:</label>
                <input
                    type="text"
                    name="username"
                    value={values.username}
                    onChange={handleFieldChange}
                    onBlur={handleFieldChange}
                    placeholder='type your username...'
                />
                {errors.username && <span>{errors.username}</span>}
            </div>

            <div>
                <label>Email:</label>
                <input
                    type="email"
                    name="email"
                    value={values.email}
                    onChange={handleFieldChange}
                    onBlur={handleFieldChange}
                    placeholder='type your email...'
                />
                {errors.email && <span>{errors.email}</span>}
            </div>

            <div>
                <label>Gender:</label>
                <select value={values.gender} name="gender" onChange={handleFieldChange}>
                    <option value="">Select</option>
                    <option value="male">Male</option>
                    <option value="female">Female</option>
                </select>
            </div>

            <div>
                <label>Password:</label>
                <input
                    type="password"
                    name="password"
                    value={values.password}
                    onChange={handleFieldChange}
                    onBlur={handleFieldChange}
                    placeholder='type your password...'
                />
                {errors.password && <span>{errors.password}</span>}
            </div>

            <div>
                <label>Retype Password:</label>
                <input
                    type="password"
                    name="retypePassword"
                    value={values.retypePassword}
                    onChange={handleFieldChange}
                    onBlur={handleFieldChange}
                    placeholder='retype your password...'
                />
                {errors.retypePassword && <span>{errors.retypePassword}</span>}
            </div>

            <div>
                <label>
                    <input
                        type="checkbox"
                        name="agreement"
                        checked={values.agreement}
                        onChange={handleFieldChange}
                        onBlur={handleFieldChange}
                    />
                    I have read the agreement
                </label>
                {errors.agreement && <span>{errors.agreement}</span>}
            </div>

            <button type="submit" disabled={!isFormValid}> Register </button>
        </form>
    );
};








// if (name === 'username') {
        //     if (!value) {
        //         errors.username = 'Username is required';
        //     } else if (!usernameRegex.test(value)) {
        //         errors.username = 'Username must be at least 4 characters and contain only A-Z, a-z, 0-9';
        //     }
        // }
    
        // if (name === 'email') {
        //     if (!value) {
        //         errors.email = 'Email is required';
        //     } else if (!emailRegex.test(value)) {
        //         errors.email = 'Email is not valid';
        //     }
        // }
    
        // if (name === 'password') {
        //     if (!value) {
        //         errors.password = 'Password is required';
        //     } else if (!passwordRegex.test(value)) {
        //         errors.password = 'Password must be at least 8 characters';
        //     }
        // }
    
        // if (name === 'retypePassword') {
        //     if (!value) {
        //         errors.retypePassword = 'Retype password is required';
        //     } else if (value !== values.password) {
        //         errors.retypePassword = 'Retype password must be equal to password';
        //     }
        // }
    
        // if (name === 'agreement') {
        //     if (!value) {
        //         errors.agreement = 'You must agree to the terms';
        //     }
        // }
        
    // const handleBlur = (event) => {
    //     const { name, value } = event.target;
    //     const usernameRegex = /^[A-Za-z0-9]+$/;
    //     const emailRegex = /\S+@\S+\.\S+/;
    //     const passwordRegex = /.{8,}/;
    //     // Validate the field
    //     let error;
    //     if (name === 'username' && value === '') {
    //         error = 'Username is required';
    //     } else if (name === 'username' && !usernameRegex.test(value)) {
    //         error = 'Username can only contain letters and numbers';
    //     } else if (name === 'username' && value.length < 4) {
    //         error = 'Username must be at least 4 characters';
    //     } else if (name === 'email' && value === '') {
    //         error = 'Email is required';
    //     } else if (name === 'email' && !emailRegex.test(value)) {
    //         error = 'Invalid email format';
    //     } else if (name === 'password' && value === '') {
    //         error = 'Password is required';
    //     } else if (name === 'password' && !passwordRegex.test(value)) {
    //         error = 'Password must be at least 8 characters';
    //     } else if (name === 'retypePassword' && value === '') {
    //         error = 'Retype password is required';
    //     } else if (name === 'retypePassword' && value !== values.password) {
    //         error = 'Passwords do not match';
    //     } else if (name === 'agreement' && !value) {
    //         error = 'You must agree to the terms and conditions';
    //     }

//     return (
//         <form onSubmit={handleSubmit}>
//             <input
//                 type="text"
//                 name="username"
//                 value={values.username}
//                 onChange={handleFieldChange}
//                 onBlur={handleBlur}
//                 placeholder="Username"
//                 required
//             />
//             {errors.username && <p>{errors.username}</p>}

//             <input
//                 type="email"
//                 name="email"
//                 value={values.email}
//                 onChange={handleFieldChange}
//                 onBlur={handleBlur}
//                 placeholder="Email"
//                 required
//             />
//             {errors.email && <p>{errors.email}</p>}

//             <select
//                 name="gender"
//                 value={values.gender}
//                 onChange={handleFieldChange}
//                 onBlur={handleBlur}
//                 required
//             >
//                 <option value="">Select Gender</option>
//                 <option value="male">Male</option>
//                 <option value="female">Female</option>
//             </select>

//             <input
//                 type="password"
//                 name="password"
//                 value={values.password}
//                 onChange={handleFieldChange}
//                 onBlur={handleBlur}
//                 placeholder="Password"
//                 required
//             />
//             {errors.password && <p>{errors.password}</p>}

//             <input
//                 type="password"
//                 name="retypePassword"
//                 value={values.retypePassword}
//                 onChange={handleFieldChange}
//                 onBlur={handleBlur}
//                 placeholder="Retype Password"
//                 required
//             />
//             {errors.retypePassword && <p>{errors.retypePassword}</p>}

//             <label>
//             <input
//                 type="checkbox"
//                 name="agreement"
//                 checked={values.agreement}
//                 onChange={handleCheckboxChange}
//                 onBlur={handleBlur}
//                 required
//             />
//                 I have read the agreement
//             </label>
//             {errors.agreement && <p>{errors.agreement}</p>}

//             <button type="submit" disabled={}>Submit</button>
//         </form>
//     );
// };

export default RegisterForm;