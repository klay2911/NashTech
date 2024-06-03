import React, { useState } from 'react';
import { createCategory } from '../../services/categories.service';
import { useNavigate } from 'react-router-dom';

const CreateCategory = () => {
 const [Name, setName] = useState('');
 const navigate = useNavigate();

  const handleSubmit = (event) => {
    event.preventDefault();

    createCategory({ Name })
      .then(response => {
        console.log("Created category:", response?.data);
        navigate('/categories');
      })
      .catch(error => {
        console.error("Error creating category:", error);
      });
  };

  return (
    <form onSubmit={handleSubmit} className="flex flex-col">
      <div>
        <label htmlFor="name" className="mb-2">Category Name : </label>
        <input 
            type="text" 
            id="name" 
            value={Name} 
            onChange={e => setName(e.target.value)} 
            className="mb-4 p-2 border border-gray-300 rounded"
        />
        </div>
      <button type="submit" className="p-2 bg-blue-500 text-white rounded">Create Category</button>
    </form>
  );
};

export default CreateCategory;
