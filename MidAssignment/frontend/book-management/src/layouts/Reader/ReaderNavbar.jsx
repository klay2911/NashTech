import React from "react";
import { NavLink } from "react-router-dom";
import { path } from "../../route/routeContants";
import { useAuthContext } from "../../context/AuthContext";

const ReaderNavbar = () => {
  const { isAuthenticated, setIsAuthenticated } = useAuthContext();
  const { user } = useAuthContext();
  const userName = user?.Name;

  const handleLogout = () => {
    localStorage.removeItem("token");
    sessionStorage.removeItem("bookIds");
    setIsAuthenticated(false);
  };

  const action = isAuthenticated
    ? [
        { name: "Wellcome, " + userName },
        { name: "HOME", path: path.home },
        { name: "BOOK", path: path.userBooks },
        { name: "REQUEST", path: path.booksUserList },
        { name: "LOGOUT", path: path.login },
      ]
    : [
        { name: "HOME", path: path.home },
        { name: "LOGIN", path: path.login },
      ];


  return (
    <div className="flex h-20 w-full flex-row items-center justify-between bg-[#F3F4F6] text-[#333333]">
      <div className="flex flex-row items-center justify-start">
        <h1 className="ml-10 text-2xl font-semibold">Library</h1>
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
export default ReaderNavbar;
