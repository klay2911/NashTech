import React, {useState, useEffect} from 'react';
import { NavLink, useLocation } from "react-router-dom";
import {path} from '../route/routeContants';
import {useAuthContext} from '../context/AuthContext';


const Navbar = () => {

  const {isAuthenticated,setIsAuthenticated} = useAuthContext();
  const [dropdownVisible, setDropdownVisible] = useState(false);
  const location = useLocation();

  const handleLogout = () => {
    localStorage.removeItem('token');
    localStorage.removeItem('userId');
    setIsAuthenticated(false);
  }
  const toggleDropdown = () => {
    setDropdownVisible(!dropdownVisible);
  }

  const action = isAuthenticated
  ? [
      { name: "HOME", path: path.home },
      { name: "BOOK", path: path.books },
      { name: "CATEGORY", path: path.categories },
      //{ name: "REQUEST", path: path.request },
    ]
  : [
      { name: "HOME", path: path.home },
      //{ name: "BOOK", path: path.books },
    ];

    useEffect(() => {
      setDropdownVisible(false);
    }, [location]);

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
          <div className="relative">
            {isAuthenticated ? (
              <>
                <button onClick={toggleDropdown} className="mr-10 text-lg font-semibold text-[#333333]">
                  PROFILE
                </button>
                {dropdownVisible && (
                  <div className="absolute right-0 w-40 mt-2 py-2 bg-white border rounded shadow-xl">
                    <NavLink to={path.profile} className="block px-4 py-2 text-[#333333] hover:bg-[#00B4D8] hover:text-white">
                      Profile
                    </NavLink>
                    {/* ... other code */}
                  </div>
                )}
              </>
            ) : null}
          </div>
        </div>
      </div>
    );
  };
  // const action = isAuthenticated
  // ? [
  //     { name: "HOME", path: path.home },
  //     { name: "BOOK", path: path.books },
  //     { name: "PROFILE", path: path.profile },
  //     { name: "LOGOUT", path: path.login },
  //   ]
  // : [
  //     { name: "HOME", path: path.home },
  //     { name: "BOOK", path: path.books },
  //     { name: "PROFILE", path: path.profile },
  //     { name: "LOGIN", path: path.login },
  //   ];
  // return (
  //   <div className="w-full h-20 flex flex-row items-center justify-between bg-gray-800 text-white">
  //   <div className="flex flex-row items-center justify-start">
  //     <h1 className="ml-10 text-2xl font-bold">Book Store</h1>
  //   </div>
  //     <div className="flex flex-row items-center justify-end">
  //       {action.map((item, index) => (
  //         <NavLink
  //           key={index}
  //           to={item.path}
  //           className="mr-10 text-lg font-semibold"
  //           activeClassName="text-red-500"
  //           onClick={item.name === "LOGOUT" ? handleLogout : null}
  //         >
  //           {item.name}
  //         </NavLink>
  //       ))}
  //     </div>
  //   </div>
  // );
  
  export default Navbar;
