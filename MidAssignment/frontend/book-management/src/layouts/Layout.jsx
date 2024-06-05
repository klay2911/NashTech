import React from "react";
import { AppRoutes, ReaderRoutes } from "../route";
import { Navbar, ReaderNavbar } from "./";
import { useAuthContext } from "../context/AuthContext";

const Layout = () => {
  const { user } = useAuthContext();
  const userRole = user?.Role;

  return (
    <div
      className="flex min-h-screen w-full flex-col items-center justify-between bg-white"
      style={{
        width: "100%",
        backgroundSize: "cover",
        backgroundAttachment: "fixed",
        backgroundRepeat: "space",
      }}
    >
      {/* <Navbar />
            <AppRoutes /> */}
      {userRole === "Reader" ? <ReaderNavbar /> : <Navbar />}
      {userRole === "Reader" ? <ReaderRoutes /> : <AppRoutes />}
      <footer className="flex h-20 w-full items-center justify-center bg-[#F3F4F6] text-[#333333]">
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
