import React, { useEffect, useState } from "react";

const Pagination = ({ postsPerPage, totalPosts, paginate, currentPage }) => {
  const pageNumbers = [];
  const [arrayPage, setarrayPage] = useState([]);

  useEffect(() => {
    let maxPage = Math.ceil(totalPosts / postsPerPage);
    let end = currentPage + 3 > maxPage ? maxPage : currentPage + 3;
    let start = currentPage - 3 > 0 ? currentPage - 3 : 1;
    let temp = [];
    for (let i = start; i <= end; i++) temp.push(i);
    setarrayPage(temp);
  }, [totalPosts, currentPage]);

  for (let i = 1; i <= Math.ceil(totalPosts / postsPerPage); i++) {
    pageNumbers.push(i);
  }

  return (
    <nav>
      <ul className="mt-4 flex justify-center">
        {currentPage - 4 > 0 && (
          <button
            onClick={() => paginate(1)}
            className={`px-3 py-1 ${
              currentPage === 1 ? "bg-blue-500 text-white" : "bg-gray-200"
            }`}
          >
            1
          </button>
        )}
        {currentPage - 4 > 0 && (
          <button disabled={true} className={`bg-gray-200} px-3 py-1`}>
            ...
          </button>
        )}
        {arrayPage.map((number) => (
          <li
            key={number}
            className={`mx-1 ${currentPage === number ? "font-bold" : ""}`}
          >
            <button
              onClick={() => paginate(number)}
              className={`px-3 py-1 ${
                currentPage === number
                  ? "bg-blue-500 text-white"
                  : "bg-gray-200"
              }`}
            >
              {number}
            </button>
          </li>
        ))}
        {currentPage + 3 < Math.ceil(totalPosts / postsPerPage) && (
          <button disabled={true} className={`bg-gray-200} px-3 py-1`}>
            ...
          </button>
        )}
        {currentPage + 3 < Math.ceil(totalPosts / postsPerPage) && (
          <button
            onClick={() => paginate(Math.ceil(totalPosts / postsPerPage))}
            className={`px-3 py-1 ${
              currentPage === Math.ceil(totalPosts / postsPerPage)
                ? "bg-blue-500 text-white"
                : "bg-gray-200"
            }`}
          >
            {Math.ceil(totalPosts / postsPerPage)}
          </button>
        )}
      </ul>
    </nav>
  );
};

export default Pagination;
