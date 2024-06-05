import React, {useState, useEffect} from 'react';
import { NavLink, useLocation } from "react-router-dom";
import {path} from '../../route/routeContants';
import {useAuthContext} from '../../context/AuthContext';

const ReaderNavbar = () => {
    const {isAuthenticated,setIsAuthenticated} = useAuthContext();
    // const location = useLocation();
    const { user } = useAuthContext();
    const userName = user?.Name;
  
    const handleLogout = () => {
      localStorage.removeItem('token');
      localStorage.removeItem('userId');
      setIsAuthenticated(false);
    }
  
    const action = isAuthenticated
    ? [
        {name: "Wellcome, " + userName},
        { name: "HOME", path: path.home },
        { name: "BOOK", path: path.userBooks },
        { name: "REQUEST", path: path.booksUserList },
        { name: "LOGOUT", path: path.login }
      ]
    : [
        { name: "HOME", path: path.home },
        { name: "LOGIN", path: path.login },
      ];
  
    //   useEffect(() => {
    //     setDropdownVisible(false);
    //   }, [location]);
  
      return (
        <div className="w-full h-20 flex flex-row items-center justify-between bg-[#F3F4F6] text-[#333333]">
          <div className="flex flex-row items-center justify-start">
            <h1 className="ml-10 text-2xl font-semibold ">Library</h1>
          </div>
          <div className="flex flex-row items-center justify-end">
            {action.map((item, index) => (
              <NavLink
                key={index}
                to={item.path}
                className="mr-10 text-lg font-semibold text-[#333333]"
                activeClassName="text-[#00B4D8]"
                onClick={item.name === "LOGOUT" ? handleLogout : null}
              >
                {item.name}
              </NavLink>
            ))}
          </div>
        </div>
      );
};
export default ReaderNavbar