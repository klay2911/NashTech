import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import { createCategory } from "../../services/categories.service";

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
    <form onSubmit={handleSubmit} className="flex flex-col">
      <div>
        <label htmlFor="name" className="mb-2">
          Category Name :{" "}
        </label>
        <input
          type="text"
          id="name"
          value={Name}
          onChange={(e) => setName(e.target.value)}
          className="mb-4 rounded border border-gray-300 p-2"
        />
      </div>
      <button type="submit" className="rounded bg-blue-500 p-2 text-white">
        Create Category
      </button>
    </form>
  );
};

export default CreateCategory;
