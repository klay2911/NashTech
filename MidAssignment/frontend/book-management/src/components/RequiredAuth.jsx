import { Navigate } from "react-router-dom";
import { useAuthContext } from "../context/AuthContext";


const RequiredAuth = ({ children }) => {
  const { isAuthenticated } = useAuthContext();

  return isAuthenticated ? children : <Navigate to="/login" />;
};

export default RequiredAuth;
