import React, { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import { Pagination, SearchBar } from "../../../components";
import { deleteBook, getListBooks } from "../../../services/books.service";

const Books = () => {
  const [books, setBooks] = useState([]);
  const [pageNumber, setPageNumber] = useState(1);
  const [pageSize, setPageSize] = useState(5);
  const [hasNextPage, setHasNextPage] = useState(false);
  const [hasPreviousPage, setHasPreviousPage] = useState(false);
  const [searchTerm, setSearchTerm] = useState("");
  const [totalCount, setTotalCount] = useState(0);

  useEffect(() => {
    fetchBooks();
  }, [pageNumber, pageSize, searchTerm]);

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

  const handlePageSizeChange = (event) => {
    setPageSize(Number(event.target.value));
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
      <SearchBar onSearch={setSearchTerm} />
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

export default Books;
