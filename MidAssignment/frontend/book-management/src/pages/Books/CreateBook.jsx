import React, { useEffect, useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { createBook } from "../../services/books.service";
import { getCategories } from "../../services/categories.service";

const CreateBook = () => {
  const [title, setTitle] = useState("");
  const [author, setAuthor] = useState("");
  const [isbn, setIsbn] = useState("");
  const [description, setDescription] = useState("");
  const [pdfFile, setPdfFile] = useState(null);
  const [coverFile, setCoverFile] = useState(null);
  const [categoryId, setCategoryId] = useState("");
  const [categories, setCategories] = useState([]);
  const navigate = useNavigate();

  useEffect(() => {
    getCategories()
      .then((response) => {
        setCategories(response?.data?.items);
      })
      .catch((error) => {
        console.error("Error fetching categories:", error);
      });
  }, []);
  const handleSubmit = (event) => {
    event.preventDefault();

    const formData = new FormData();
    formData.append("title", title);
    formData.append("author", author);
    formData.append("isbn", isbn);
    formData.append("description", description);
    formData.append("categoryId", categoryId);
    formData.append("pdfFile", pdfFile);
    formData.append("coverFile", coverFile);

    createBook(formData)
      .then((response) => {
        console.log("Created book:", response?.data);
        navigate("/books");
      })
      .catch((error) => {
        console.error("Error creating book:", error);
      });
  };

  return (
    <div className="flex h-screen flex-col items-center justify-center bg-gray-100">
      <form
        onSubmit={handleSubmit}
        className="w-full max-w-md rounded bg-white p-6 shadow-md"
      >
        <h2 className="mb-4 text-2xl font-bold">Create Book</h2>

        <label className="mb-2 block" htmlFor="title">
          Title
        </label>
        <input
          className="mb-6 w-full rounded border p-2"
          id="title"
          type="text"
        />

        <label className="mb-2 block" htmlFor="author">
          Author
        </label>
        <input
          className="mb-6 w-full rounded border p-2"
          id="author"
          type="text"
        />

        <label className="mb-2 block" htmlFor="isbn">
          ISBN
        </label>
        <input
          className="mb-6 w-full rounded border p-2"
          id="isbn"
          type="text"
        />

        <label className="mb-2 block" htmlFor="description">
          Description
        </label>
        <input
          className="mb-6 w-full rounded border p-2"
          id="description"
          type="text"
        />

        <label className="mb-2 block" htmlFor="category">
          Category
        </label>
        <select
          className="mb-6 w-full rounded border p-2"
          id="category"
        ></select>

        <label className="mb-2 block" htmlFor="pdfFile">
          PDF File
        </label>
        <input
          className="mb-6 w-full rounded border p-2"
          id="pdfFile"
          type="file"
        />

        <label className="mb-2 block" htmlFor="coverFile">
          Cover File
        </label>
        <input
          className="mb-6 w-full rounded border p-2"
          id="coverFile"
          type="file"
        />

        <button
          type="submit"
          className="mb-4 w-full rounded bg-blue-500 p-2 text-white"
        >
          Create Book
        </button>

        <Link
          to="/books"
          className="w-full rounded bg-gray-500 p-2 text-center text-white"
        >
          Back
        </Link>
      </form>
    </div>
  );
};

export default CreateBook;
