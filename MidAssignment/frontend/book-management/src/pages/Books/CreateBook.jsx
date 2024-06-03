import React, { useState, useEffect } from 'react';
import { createBook } from '../../services/books.service';
import { getCategories } from '../../services/categories.service';
import { useNavigate } from 'react-router-dom';

const CreateBook = () => {
  const [title, setTitle] = useState('');
  const [author, setAuthor] = useState('');
  const [isbn, setIsbn] = useState('');
  const [description, setDescription] = useState('');
  const [pdfFile, setPdfFile] = useState(null);
  const [coverFile, setCoverFile] = useState(null);
  const [categoryId, setCategoryId] = useState('');
  const [categories, setCategories] = useState([]);
  const navigate = useNavigate();

  useEffect(() => {
    getCategories()
      .then(response => {
        setCategories(response?.data?.items);
      })
      .catch(error => {
        console.error("Error fetching categories:", error);
      });
  }, []);
  const handleSubmit = (event) => {
    event.preventDefault();

    const formData = new FormData();
    formData.append('title', title);
    formData.append('author', author);
    formData.append('isbn', isbn);
    formData.append('description', description);
    formData.append('categoryId', categoryId);
    formData.append('pdfFile', pdfFile); 
    formData.append('coverFile', coverFile); 
    //formData.append('name', 'Thien Vu'); 

  createBook(formData)
    .then(response => {
      console.log("Created book:", response?.data);
      navigate('/books');
    })
    .catch(error => {
      console.error("Error creating book:", error);
    });
};

  return (
    <form onSubmit={handleSubmit} className="flex flex-col">
      <div>
        <label htmlFor="title" className="mb-2">Book Title : </label>
        <input 
            type="text" 
            id="title" 
            value={title} 
            onChange={e => setTitle(e.target.value)} 
            className="mb-4 p-2 border border-gray-300 rounded"
        />
        <label htmlFor="title" className="mb-2">Author : </label>
        <input 
            type="text" 
            id="author" 
            value={author} 
            onChange={e => setAuthor(e.target.value)} 
            className="mb-4 p-2 border border-gray-300 rounded"
        />
        <label htmlFor="title" className="mb-2">ISBN : </label>
        <input 
            type="text" 
            id="isbn" 
            value={isbn} 
            onChange={e => setIsbn(e.target.value)} 
            className="mb-4 p-2 border border-gray-300 rounded"
        />
        <label htmlFor="title" className="mb-2">Description : </label>
        <input 
            type="text" 
            id="description" 
            value={description} 
            onChange={e => setDescription(e.target.value)} 
            className="mb-4 p-2 border border-gray-300 rounded"
        />
        <label htmlFor="category" className="mb-2">Category : </label>
        <select 
            id="category" 
            value={categoryId} 
            onChange={e => setCategoryId(e.target.value)} 
            className="mb-4 p-2 border border-gray-300 rounded"
        >
          {categories.map(category => (
            <option key={category.categoryId} value={category.categoryId}>
              {category.name}
            </option>
          ))}
        </select>
        <label htmlFor="pdfFile" className="mb-2">PDF File : </label>
<input 
    type="file" 
    id="pdfFile" 
    onChange={e => setPdfFile(e.target.files[0])} 
    className="mb-4 p-2 border border-gray-300 rounded"
/>

<label htmlFor="coverFile" className="mb-2">Cover File : </label>
<input 
    type="file" 
    id="coverFile" 
    onChange={e => setCoverFile(e.target.files[0])} 
    className="mb-4 p-2 border border-gray-300 rounded"
/>
      </div>
      <button type="submit" className="p-2 bg-blue-500 text-white rounded">Create Book</button>
    </form>
  );
};

export default CreateBook;
