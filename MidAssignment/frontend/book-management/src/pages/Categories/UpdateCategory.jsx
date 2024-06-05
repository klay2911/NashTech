import React, { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { getCategory, updateCategory } from "../../services/categories.service";

const UpdateCategory = ({}) => {
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
    <form onSubmit={handleSubmit}>
      <div>
        <label htmlFor="name">Category Name : </label>
        <input
          type="text"
          id="name"
          value={Name}
          onChange={(e) => setName(e.target.value)}
        />
      </div>
      <button type="submit">Update Category</button>
    </form>
  );
};

export default UpdateCategory;
