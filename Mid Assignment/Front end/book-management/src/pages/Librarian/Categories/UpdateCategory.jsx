import React, { useEffect, useState } from "react";
import { Link, useNavigate, useParams } from "react-router-dom";
import {
  getCategory,
  updateCategory,
} from "../../../services/categories.service";

const UpdateCategory = () => {
  const [Name, setName] = useState("");
  const navigate = useNavigate();
  const { id } = useParams();

  useEffect(() => {
    getCategory(id)
      .then((response) => {
        setName(response?.data?.name);
      })
      .catch((error) => {
        console.error("Error fetching category:", error);
      });
  }, [id]);

  const handleSubmit = (event) => {
    event.preventDefault();

    updateCategory(id, { Name })
      .then((response) => {
        console.log("Updated category:", response?.data);
        navigate("/categories");
      })
      .catch((error) => {
        console.error("Error updating category:", error);
      });
  };

  return (
    <div className="flex flex-col">
      <form
        onSubmit={handleSubmit}
        className="animate-fade-in-down space-y-4 p-6 transition-all duration-500"
      >
        <div className="flex flex-col">
          <label htmlFor="name" className="mb-2 font-semibold text-gray-700">
            Category Name :
          </label>
          <input
            type="text"
            id="name"
            value={Name}
            onChange={(e) => setName(e.target.value)}
            className="rounded-md border border-gray-300 p-2 transition-all duration-200 focus:outline-none focus:ring-2 focus:ring-blue-500"
          />
        </div>
        <button
          type="submit"
          className="rounded-md bg-blue-500 p-2 text-white transition-all duration-200 hover:bg-blue-600"
        >
          Update Category
        </button>
      </form>
      <div className="mt-8 flex justify-center">
        <Link
          to="/categories"
          className="w-120 transform rounded bg-gray-500 p-2 text-center text-white transition-colors duration-150 ease-linear hover:scale-105 hover:bg-gray-600"
        >
          Back
        </Link>
      </div>
    </div>
  );
};

export default UpdateCategory;
