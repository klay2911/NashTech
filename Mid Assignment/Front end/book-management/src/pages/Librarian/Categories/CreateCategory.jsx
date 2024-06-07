import React, { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { createCategory } from "../../../services/categories.service";

const CreateCategory = () => {
  const [Name, setName] = useState("");
  const navigate = useNavigate();

  const handleSubmit = (event) => {
    event.preventDefault();

    createCategory({ Name })
      .then((response) => {
        console.log("Created category:", response?.data);
        navigate("/categories");
      })
      .catch((error) => {
        console.error("Error creating category:", error);
      });
  };

  return (
    <div className="flex flex-col items-center justify-center space-y-4">
      <form
        onSubmit={handleSubmit}
        className="flex flex-col items-center justify-center space-y-4"
      >
        <div className="flex flex-col items-start">
          <label htmlFor="name" className="mb-2 text-lg font-semibold">
            Category Name :{" "}
          </label>
          <input
            type="text"
            id="name"
            value={Name}
            onChange={(e) => setName(e.target.value)}
            className="mb-4 w-full rounded border border-gray-300 p-2"
          />
        </div>
        <button
          type="submit"
          className="w-full rounded bg-blue-500 p-2 text-white transition duration-200 ease-in-out hover:bg-blue-600"
        >
          Create Category
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

export default CreateCategory;
