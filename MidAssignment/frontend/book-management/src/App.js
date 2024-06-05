import React from 'react';
import './App.css';
import AuthProvider from './context/AuthContext';
import { Layout } from "./layouts";



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
