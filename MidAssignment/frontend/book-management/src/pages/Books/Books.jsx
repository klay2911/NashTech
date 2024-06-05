import React, { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import { deleteBook, getListBooks } from "../../services/books.service";

const Books = () => {
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

  const handleNext = () => {
    if (hasNextPage) {
      setPageNumber((prevPageNumber) => prevPageNumber + 1);
    }
  };

  const handlePrevious = () => {
    if (hasPreviousPage && pageNumber > 1) {
      setPageNumber((prevPageNumber) => prevPageNumber - 1);
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

  const handleDelete = (BookId) => {
    if (window.confirm("Do you want to delete this book?")) {
      deleteBook(BookId)
        .then(() => {
          fetchBooks();
        })
        .catch((error) => {
          console.error("Error deleting Book:", error);
        });
    }
  };
  return (
    <div className="flex flex-col">
      <div className="-my-2 overflow-x-auto sm:-mx-6 lg:-mx-8">
        <div className="inline-block min-w-full py-2 align-middle sm:px-6 lg:px-8">
          <div className="flex justify-end pb-4">
            <button className="rounded bg-blue-500 px-4 py-2 font-bold text-white hover:bg-blue-700">
              <Link to={`/book/create`}>Add new</Link>
            </button>
          </div>
          <div className="overflow-hidden border-b border-gray-200 shadow sm:rounded-lg">
            <table className="min-w-full divide-y divide-gray-200">
              <thead className="bg-gray-50">
                <tr>
                  <th
                    scope="col"
                    className="px-6 py-3 text-left text-xs font-medium uppercase tracking-wider text-gray-500"
                  >
                    Id
                  </th>
                  <th
                    scope="col"
                    className="px-6 py-3 text-left text-xs font-medium uppercase tracking-wider text-gray-500"
                  >
                    Title
                  </th>
                  <th
                    scope="col"
                    className="px-6 py-3 text-left text-xs font-medium uppercase tracking-wider text-gray-500"
                  >
                    Author
                  </th>
                  <th
                    scope="col"
                    className="px-6 py-3 text-left text-xs font-medium uppercase tracking-wider text-gray-500"
                  >
                    ISBN
                  </th>
                  <th
                    scope="col"
                    className="px-6 py-3 text-left text-xs font-medium uppercase tracking-wider text-gray-500"
                  >
                    Description
                  </th>
                  <th
                    scope="col"
                    className="px-6 py-3 text-left text-xs font-medium uppercase tracking-wider text-gray-500"
                  >
                    Category Name
                  </th>
                  <th
                    scope="col"
                    className="px-6 py-3 text-left text-xs font-medium uppercase tracking-wider text-gray-500"
                  >
                    Action
                  </th>
                </tr>
              </thead>
              <tbody className="divide-y divide-gray-200 bg-[#F3F4F6]">
                {books.map((book, index) => (
                  <tr key={book.id}>
                    <td className="whitespace-nowrap px-6 py-4 text-sm text-gray-500">
                      {index + 1}
                    </td>
                    <td className="whitespace-nowrap px-6 py-4 text-sm text-gray-500">
                      {book.title}
                    </td>
                    <td className="whitespace-nowrap px-6 py-4 text-sm text-gray-500">
                      {book.author}
                    </td>
                    <td className="whitespace-nowrap px-6 py-4 text-sm text-gray-500">
                      {book.isbn}
                    </td>
                    <td className="whitespace-nowrap px-6 py-4 text-sm text-gray-500">
                      {book.description}
                    </td>
                    <td className="whitespace-nowrap px-6 py-4 text-sm text-gray-500">
                      {book?.category?.name}
                    </td>
                    <td className="whitespace-nowrap px-6 py-4 text-sm text-gray-500">
                      <button className="mr-2 rounded bg-blue-500 px-2 py-1 font-bold text-white hover:bg-blue-700">
                        <Link to={`/book/edit/${book.id}`}>Edit</Link>
                      </button>
                      <button
                        onClick={() => handleDelete(book.id)}
                        className="mr-2 rounded bg-red-500 px-2 py-1 font-bold text-white hover:bg-red-700"
                      >
                        Delete
                      </button>
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        </div>
      </div>
      <div className="flex">
        <div className="w-1/4 p-2">
          <button
            onClick={handlePrevious}
            disabled={!hasPreviousPage}
            className={`w-full rounded bg-blue-500 px-4 py-2 font-bold text-white hover:bg-blue-700 ${!hasPreviousPage ? "cursor-not-allowed opacity-50" : ""}`}
          >
            Previous
          </button>
        </div>
        <div className="flex w-1/2 items-center justify-center p-2">
          {/* <span>Page {currentPage}</span> */}
        </div>
        <div className="w-1/4 p-2">
          <button
            onClick={handleNext}
            disabled={!hasNextPage}
            className={`w-full rounded bg-blue-500 px-4 py-2 font-bold text-white hover:bg-blue-700 ${!hasNextPage ? "cursor-not-allowed opacity-50" : ""}`}
          >
            Next
          </button>
        </div>
      </div>
      <div style={{ position: "fixed", right: -50, bottom: 80 }}>
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

export default Books;
