import React from 'react'
import Navbar  from './Navbar'
import {AppRoutes} from '../route/AppRoutes'
//call API logout
//clear localstorage
//set isAuthenticated false
//navigate to login
//const [isAuthenticated, setIsAuthenticated] = useState(!!localStorage.getItem('token'));

const Layout = () => {
    return (
        <div className="w-full flex flex-col justify-between items-center min-h-screen bg-white"
        style ={{
        width:'100%',
        backgroundSize:'cover',
        backgroundAttachment:'fixed',
        backgroundRepeat:'space'}}
        >
            <Navbar/>
            <AppRoutes/>
            <footer className="w-full h-20 bg-[#F3F4F6] flex justify-center items-center text-[#333333]">
                <p>© 2024 - All rights reserved</p>
            </footer>
        </div>
    );
};

export default Layout;