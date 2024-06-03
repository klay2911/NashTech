import './App.css';
import React from 'react';
import { Layout} from "./layouts";
import AuthProvider from './context/AuthContext';



function App() {
  return (
    <AuthProvider>
      <div className='App h-full w-full'>
        <Layout/>
      </div>
    </AuthProvider>
  )  
}

export default App;
