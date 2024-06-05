import React, { useEffect, useState } from "react";
import { EyeFill } from "react-bootstrap-icons";
import { Link } from "react-router-dom";
import { getListBooks } from "../../services/books.service";
import { createBorrowRequest } from "../../services/requests.service";

const BorrowingBooks = () => {
  const [bookIds, setBookIds] = useState(
    JSON.parse(sessionStorage.getItem("bookIds")) || [],
  );
  const [books, setBooks] = useState([]);
  const [pageNumber, setPageNumber] = useState(1);
  const [pageSize, setPageSize] = useState(5);
  const [inputValue, setInputValue] = useState(pageSize);

  const fetchBooks = async () => {
    try {
      const response = await getListBooks(pageNumber, pageSize);
      setBooks(response?.data?.items);
    } catch (error) {
      console.error("Error fetching books:", error);
    }
  };
  const handleClick = async () => {
    const bookIds = JSON.parse(sessionStorage.getItem("bookIds"));
    try {
      const response = await createBorrowRequest(bookIds);
      if (typeof response?.data === "string") {
        alert(response.data);
      } else {
        console.log(response);
      }
    } catch (error) {
      console.error(error);
    }
  };

  const handlePageSizeChange = (event) => {
    setInputValue(event.target.value);
  };
  const handleSubmit = (event) => {
    event.preventDefault();
    setPageSize(inputValue);
    setPageNumber(1);
  };
  const handleAddToRequest = (bookId) => {
    const updatedBookIds = [...bookIds, bookId];
    setBookIds(updatedBookIds);
    //object json
    sessionStorage.setItem("bookIds", JSON.stringify(updatedBookIds));
  };

  const handleRemoveFromRequest = (bookId) => {
    //array method
    const updatedBookIds = bookIds.filter((id) => id !== bookId);
    setBookIds(updatedBookIds);
    sessionStorage.setItem("bookIds", JSON.stringify(updatedBookIds));
  };

  useEffect(() => {
    fetchBooks();
  }, [pageNumber, pageSize]);
  return (
    <div className="flex flex-col">
      <div className="m-4 self-end">
        <button
          onClick={handleClick}
          className="rounded bg-blue-500 px-4 py-2 font-bold text-white hover:bg-blue-700"
        >
          Request
        </button>
      </div>
      <div className="grid grid-cols-5 gap-4">
        {books.map((book) => (
          <div key={book?.id} className="rounded bg-white p-4 shadow">
            <img
              src={
                book?.coverPath
                  ? `${process.env.REACT_APP_API_BASE_URL}${book.coverPath}`
                  : "https://localhost:44352/covers/Cover.png"
              }
              alt={book.title}
              className="mb-4 h-40 w-full object-cover"
            />
            <h3 className="mb-2 text-xl font-bold">{book.title}</h3>
            <p className="mb-4 text-gray-500">{book.author}</p>
            <div className="flex justify-between">
              {bookIds.includes(book.id) ? (
                <button
                  className="rounded bg-red-500 px-4 py-2 font-bold text-white hover:bg-red-600"
                  onClick={() => handleRemoveFromRequest(book.id)}
                >
                  Remove
                </button>
              ) : (
                <button
                  className="rounded bg-blue-500 px-4 py-2 font-bold text-white hover:bg-blue-600"
                  onClick={() => handleAddToRequest(book.id)}
                >
                  Add to Request
                </button>
              )}
              <Link
                to={`/books/details/${book.id}`}
                className="inline-flex items-center justify-center rounded bg-gray-500 px-4 py-2 font-bold text-white hover:bg-gray-600"
              >
                <EyeFill className="mr-2" />
              </Link>
            </div>
          </div>
        ))}
      </div>
      <div className="m-4 self-end">
        <form onSubmit={handleSubmit}>
          <input
            type="number"
            min="1"
            value={inputValue}
            onChange={handlePageSizeChange}
            className="dark:bg-[#F3F4F6]-700 w-1/2 rounded-md border border-gray-300 px-3 py-2 placeholder-gray-300 focus:border-indigo-300 focus:outline-none focus:ring focus:ring-indigo-100 dark:border-gray-600 dark:text-[#333333] dark:placeholder-gray-500 dark:focus:border-gray-500 dark:focus:ring-gray-900"
          />
          <button type="submit" style={{ display: "none" }} />
        </form>
      </div>
    </div>
  );
};

export default BorrowingBooks;
