import React, { useEffect} from "react";

const Pagination = ({
  pageNumber,
  hasNextPage,
  hasPreviousPage,
  setPageNumber,
  pageSize,
  handlePageSizeChange,
  totalItems,
  fetchData,
}) => {
  useEffect(() => {
    fetchData();
  }, [pageNumber, pageSize]);

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

  const totalPages = Math.ceil(totalItems / pageSize);

  return (
    <div className="my-4 flex justify-center space-x-4">
      <button
        onClick={handlePrevious}
        disabled={!hasPreviousPage}
        className="px-4 py-2 text-gray-700 transition-colors duration-150 ease-linear hover:bg-blue-600 hover:text-white"
      >
        Previous
      </button>
      <span className="px-4 py-2 text-gray-700">
        {pageNumber} / {totalPages} {/* display current page and total pages */}
      </span>
      <button
        onClick={handleNext}
        disabled={!hasNextPage}
        className="px-4 py-2 text-gray-700 transition-colors duration-150 ease-linear hover:bg-blue-600 hover:text-white"
      >
        Next
      </button>
      <select
        value={pageSize}
        onChange={handlePageSizeChange}
        className="ml-2 rounded-md border text-gray-700 transition-colors duration-150 ease-linear hover:border-blue-600"
      >
        {[5, 10, 15, 20].map((size) => (
          <option key={size} value={size}>
            Show {size}
          </option>
        ))}
      </select>
    </div>
  );
};

export default Pagination;