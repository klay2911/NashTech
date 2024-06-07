import React, { useEffect, useState } from "react";
import { EyeFill } from "react-bootstrap-icons";
import { Link } from "react-router-dom";
import { Pagination, SearchBar } from "../../components";
import { getListBooks } from "../../services/books.service";
import { createBorrowRequest } from "../../services/requests.service";

const BorrowingBooks = () => {
  const [bookIds, setBookIds] = useState(
    JSON.parse(sessionStorage.getItem("bookIds")) || [],
  );
  const [books, setBooks] = useState([]);
  const [pageNumber, setPageNumber] = useState(1);
  const [pageSize, setPageSize] = useState(5);
  const [hasNextPage, setHasNextPage] = useState(false);
  const [hasPreviousPage, setHasPreviousPage] = useState(false);
  const [searchTerm, setSearchTerm] = useState("");
  const [totalCount, setTotalCount] = useState(0);


  const fetchBooks = async () => {
    try {
      const response = await getListBooks(pageNumber, pageSize, searchTerm);
      setBooks(response?.data?.items);
      setHasNextPage(response?.data?.hasNextPage);
      setHasPreviousPage(response?.data?.hasPreviousPage);
      setTotalCount(response?.data?.totalCount);

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
        sessionStorage.removeItem("bookIds");
        setBookIds([]);
      } else {
        console.log(response);
      }
    } catch (error) {
      console.error(error);
    }
  };
  const handlePageSizeChange = (event) => {
    setPageSize(Number(event.target.value));
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
  }, [pageNumber, pageSize, searchTerm]);

  return (
    <div className="flex flex-col">
      <SearchBar onSearch={setSearchTerm} />
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
            <h3 className="mb-2 text-xl font-bold">{book?.title}</h3>
            <p className="mb-4 text-gray-500">{book?.author}</p>
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
                to={`/books/details/${book?.id}`}
                className="inline-flex items-center justify-center rounded bg-gray-500 px-4 py-2 font-bold text-white hover:bg-gray-600"
              >
                <EyeFill className="mr-2" />
              </Link>
            </div>
          </div>
        ))}
      </div>
      <Pagination
        pageNumber={pageNumber}
        hasNextPage={hasNextPage}
        hasPreviousPage={hasPreviousPage}
        setPageNumber={setPageNumber}
        pageSize={pageSize}
        handlePageSizeChange={handlePageSizeChange}
        totalItems={totalCount}
        fetchData={fetchBooks}
      />
    </div>
  );
};

export default BorrowingBooks;
