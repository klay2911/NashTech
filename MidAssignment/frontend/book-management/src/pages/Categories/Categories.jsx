import React, { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import {
  deleteCategory,
  getCategories,
} from "../../services/categories.service";

const Categories = () => {
  const [categories, setCategories] = useState([]);
  const [pageNumber, setPageNumber] = useState(1);
  const [pageSize, setPageSize] = useState(5);
  const [inputValue, setInputValue] = useState(pageSize);
  const [hasNextPage, setHasNextPage] = useState(false);
  const [hasPreviousPage, setHasPreviousPage] = useState(false);

  useEffect(() => {
    fetchCategories();
  }, [pageNumber, pageSize]);

  const fetchCategories = async () => {
    try {
      const response = await getCategories(pageNumber, pageSize);
      setCategories(response?.data?.items);
      setHasNextPage(response?.data?.hasNextPage);
      setHasPreviousPage(response?.data?.hasPreviousPage);
    } catch (error) {
      console.error("Error fetching categories:", error);
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

  const handleDelete = (categoryId) => {
    if (window.confirm("Do you want to delete this category?")) {
      deleteCategory(categoryId)
        .then(() => {
          fetchCategories();
        })
        .catch((error) => {
          console.error("Error deleting category:", error);
        });
    }
  };
  return (
    <div className="flex flex-col">
      <div className="-my-2 overflow-x-auto sm:-mx-6 lg:-mx-8">
        <div className="inline-block min-w-full py-2 align-middle sm:px-6 lg:px-8">
          <div className="flex justify-end pb-4">
            <form onSubmit={handleSubmit}>
              <input
                type="number"
                min="1"
                value={inputValue}
                onChange={handlePageSizeChange}
                className="w-full rounded-md border border-gray-300 px-3 py-2 placeholder-gray-300 focus:border-indigo-300 focus:outline-none focus:ring focus:ring-indigo-100 dark:border-gray-600 dark:bg-gray-700 dark:text-white dark:placeholder-gray-500 dark:focus:border-gray-500 dark:focus:ring-gray-900"
              />
              <button type="submit" style={{ display: "none" }} />
            </form>
            <button className="rounded bg-blue-500 px-4 py-2 font-bold text-white hover:bg-blue-700">
              <Link to={`/category/create`}>Add new</Link>
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
                    Name
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
                {categories.map((category, index) => (
                  <tr key={category.id}>
                    <td className="whitespace-nowrap px-6 py-4 text-sm text-gray-500">
                      {index + 1}
                    </td>
                    <td className="whitespace-nowrap px-6 py-4 text-sm text-gray-500">
                      {category.name}
                    </td>
                    <td className="whitespace-nowrap px-6 py-4 text-sm text-gray-500">
                      <button className="mr-2 rounded bg-blue-500 px-2 py-1 font-bold text-white hover:bg-blue-700">
                        <Link to={`/category/edit/${category.categoryId}`}>
                          Edit
                        </Link>
                      </button>
                      <button
                        onClick={() => handleDelete(category.categoryId)}
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
        <div className="w-1/2 p-2">
          <button
            onClick={handlePrevious}
            disabled={!hasPreviousPage}
            className={`w-full rounded bg-blue-500 px-4 py-2 font-bold text-white hover:bg-blue-700 ${!hasPreviousPage ? "cursor-not-allowed opacity-50" : ""}`}
          >
            Previous
          </button>
        </div>
        <div className="w-1/2 p-2">
          <button
            onClick={handleNext}
            disabled={!hasNextPage}
            className={`w-full rounded bg-blue-500 px-4 py-2 font-bold text-white hover:bg-blue-700 ${!hasNextPage ? "cursor-not-allowed opacity-50" : ""}`}
          >
            Next
          </button>
        </div>
      </div>
    </div>
  );
};

export default Categories;
