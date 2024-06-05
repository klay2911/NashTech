import React, { useState, useEffect } from 'react';
import { getListBooks } from '../../services/books.service';
import {createBorrowRequest} from '../../services/requests.service';
import {Paginations} from '../../components';
import { Link } from 'react-router-dom';
import { EyeFill } from 'react-bootstrap-icons';



const BorrowingBooks = () => {
    const [bookIds, setBookIds] = useState([]);
    const [books, setBooks] = useState([]);
    const [pageNumber, setPageNumber] = useState(1);
    const [pageSize, setPageSize] = useState(5);
    const [inputValue, setInputValue] = useState(pageSize);
    const [hasNextPage, setHasNextPage] = useState(false);
    const [hasPreviousPage, setHasPreviousPage] = useState(false);


  useEffect(() => {
    fetchBooks();
  }, [pageNumber, pageSize]);

  const fetchBooks = async () => {
    try {
      const response = await getListBooks(pageNumber, pageSize);
      setBooks(response?.data?.items);
      setHasNextPage(response?.data?.hasNextPage);
      setHasPreviousPage(response?.data?.hasPreviousPage);
    } catch (error) {
      console.error("Error fetching books:", error);
    }
  };
  const handleClick = async () => {
    const bookIds = JSON.parse(sessionStorage.getItem('bookIds'));
    const request = { bookIds };
    //console.log(request);
    // Check if no books are selected
    if (!bookIds || bookIds.length === 0) {
      alert('No books selected for borrowing.');
      return;
  }

    // Check if more than 5 books are selected
    if (bookIds.length > 5) {
        alert('You can borrow maximum 5 books in 1 request');
        return;
    }
  try {
    const response = await createBorrowRequest(request);
    if (typeof response.data === 'string') {
      alert(response.data); // Show the error message
    } else {
      console.log(response);
    }
    } catch (error) {
      console.error(error);
    }
  };

  const handleNext = () => {
    if (hasNextPage) {
      setPageNumber(prevPageNumber => prevPageNumber + 1);
    }
  };

  const handlePrevious = () => {
    if (hasPreviousPage && pageNumber > 1) {
      setPageNumber(prevPageNumber => prevPageNumber - 1);
    }
  };

  const handlePageSizeChange = (event) => {
    setInputValue(event.target.value);;
  };
  const handleSubmit = (event) => {
    event.preventDefault();
    setPageSize(inputValue);
    setPageNumber(1); 
  };
    const handleAddToRequest = (bookId) => {
        const updatedBookIds = [...bookIds, bookId];
        setBookIds(updatedBookIds);
        sessionStorage.setItem('bookIds', JSON.stringify(updatedBookIds));
    };

    const handleRemoveFromRequest = (bookId) => {
        const updatedBookIds = bookIds.filter((id) => id !== bookId);
        setBookIds(updatedBookIds);
        sessionStorage.setItem('bookIds', JSON.stringify(updatedBookIds));
    };

    return (
      <div className="flex flex-col">
        <div className="self-end m-4">
            <button 
                onClick={handleClick} 
                className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded"
            >
                Request
            </button>
        </div>
        <div className="grid grid-cols-5 gap-4">  
            {books.map((book) => (
                <div key={book?.id} className="bg-white p-4 rounded shadow">
                        <img src={book?.coverPath ? `${process.env.REACT_APP_API_BASE_URL}${book.coverPath}` : 'https://localhost:44352/covers/Cover.png'} alt={book.title} className="w-full h-40 object-cover mb-4" />
                    <h3 className="text-xl font-bold mb-2">{book.title}</h3>
                    <p className="text-gray-500 mb-4">{book.author}</p>
                    <div className="flex justify-between">
                        {bookIds.includes(book.id) ? (
                            <button
                                className="bg-red-500 hover:bg-red-600 text-white font-bold py-2 px-4 rounded"
                                onClick={() => handleRemoveFromRequest(book.id)}
                            >
                                Remove
                            </button>
                        ) : (
                            <button
                                className="bg-blue-500 hover:bg-blue-600 text-white font-bold py-2 px-4 rounded"
                                onClick={() => handleAddToRequest(book.id)}
                            >
                                Add to Request
                            </button>
                        )}
                        <Link to={`/books/details/${book.id}`} className="bg-gray-500 hover:bg-gray-600 text-white font-bold py-2 px-4 rounded inline-flex justify-center items-center">
                          <EyeFill className="mr-2" />
                      </Link>
                    </div>
                </div>
            ))}
            </div>
        <div className="self-end m-4">
            <form onSubmit={handleSubmit}>
                <input 
                    type="number" 
                    min="1"
                    value={inputValue} 
                    onChange={handlePageSizeChange} 
                    className="w-1/2 px-3 py-2 placeholder-gray-300 border border-gray-300 rounded-md focus:outline-none focus:ring focus:ring-indigo-100 focus:border-indigo-300 dark:bg-[#F3F4F6]-700 dark:text-[#333333] dark:placeholder-gray-500 dark:border-gray-600 dark:focus:ring-gray-900 dark:focus:border-gray-500"
                />
                <button type="submit" style={{ display: 'none' }} />
            </form>
        </div>
    </div>
  );
};

export default BorrowingBooks;
