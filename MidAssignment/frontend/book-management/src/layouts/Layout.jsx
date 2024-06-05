import React from 'react'
import {AppRoutes, ReaderRoutes} from '../route'
import { Navbar, ReaderNavbar } from './';
import{ useAuthContext }  from '../context/AuthContext';




const Layout = () => {
    const { user } = useAuthContext();
    const userRole = user?.Role;

    return (
        <div className="w-full flex flex-col justify-between items-center min-h-screen bg-white"
        style ={{
        width:'100%',
        backgroundSize:'cover',
        backgroundAttachment:'fixed',
        backgroundRepeat:'space'}}
        >
            {/* <Navbar />
            <AppRoutes /> */}
            {userRole === 'Reader' ? <ReaderNavbar /> : <Navbar />}
            {userRole === 'Reader' ? <ReaderRoutes /> : <AppRoutes />}
            <footer className="w-full h-20 bg-[#F3F4F6] flex justify-center items-center text-[#333333]">
                <p>© 2024 - All rights reserved</p>
            </footer>
        </div>
    );
};

export default Layout;


//call API logout
//clear localstorage
//set isAuthenticated false
//navigate to login
//const [isAuthenticated, setIsAuthenticated] = useState(!!localStorage.getItem('token'));