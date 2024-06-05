import { createContext, useContext, useEffect, useState } from "react";
import {jwtDecode} from 'jwt-decode';

const AuthContext = createContext({
    isAuthenticated: false,
    setIsAuthenticated: () => {},
    user: {NameIdentifier: '', Name: '', Role:''},
    setUser: () => {}
});
export const useAuthContext = () => useContext(AuthContext);
const AuthProvider = (props) => {
    const [isAuthenticated, setIsAuthenticated] = useState();//!!token
    const [user, setUser] = useState({NameIdentifier: '', Name: '', Role: 0});

    useEffect(() => {
        // call api
        // set user(data)
        // set isAuthenticated true
        const token = localStorage.getItem('token')
        if(token){
            const decodedToken = jwtDecode(token);
            const userId = decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'];
            const name = decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'];
            const role = decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];    
            setIsAuthenticated(true);
            setUser({ NameIdentifier: userId, Name: name, Role: role });
            // console.log('userId:', userId);
            // console.log('name:', name);
            // console.log('role:', role);
        }
    },[isAuthenticated]);
    return (
    <AuthContext.Provider value={{isAuthenticated, setIsAuthenticated, user, setUser}}>
        {props.children}
        </AuthContext.Provider>
    );
};
export default AuthProvider;