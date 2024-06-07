import React, { useEffect, useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { createBook } from "../../../services/books.service";
import { getCategories } from "../../../services/categories.service";

const CreateBook = () => {
  const [form, setForm] = useState({
    title: "",
    author: "",
    isbn: "",
    description: "",
    categoryId: "",
    pdfFile: null,
    coverFile: null,
  });
  const [categories, setCategories] = useState([]);
  const navigate = useNavigate();
  const [errors, setErrors] = useState({});

  useEffect(() => {
    getCategories()
      .then((response) => {
        setCategories(response?.data?.items);
      })
      .catch((error) => {
        console.error("Error fetching categories:", error);
      });
  }, []);

  function validateForm() {
    let formErrors = {};

    if (!form.title) formErrors.title = "Title is required";
    if (!form.author) formErrors.author = "Author is required";
    if (!form.categoryId) formErrors.category = "Category is required";
    if (!form.pdfFile) formErrors.pdfFile = "PDF File is required";

    setErrors(formErrors);

    return Object.keys(formErrors).length === 0;
  }

  const handleSubmit = (event) => {
    event.preventDefault();
    if (!validateForm()) return;

    const formData = new FormData();
    formData.append("title", form.title);
    formData.append("author", form.author);
    formData.append("isbn", form.isbn);
    formData.append("description", form.description);
    formData.append("categoryId", form.categoryId);
    formData.append("pdfFile", form.pdfFile);
    formData.append("coverFile", form.coverFile);

    createBook(formData)
      .then(() => {
        alert(`Book ${form.title} created successfully`);
        navigate("/books");
      })
      .catch((error) => {
        console.error("Error creating book:", error);
      });
  };

  return (
    <div>
      <div className="flex items-start">
        <Link
          to="/books"
          className="rounded bg-gray-500 p-2 text-white transition-colors duration-150 ease-linear hover:bg-gray-600"
        >
          Back
        </Link>
      </div>
      <div className="flex h-auto flex-col items-center justify-center bg-gray-100">
        <form
          onSubmit={handleSubmit}
          className="w-full max-w-md transform rounded bg-white p-6 shadow-md transition-all duration-500 ease-in-out hover:scale-105"
        >
          <h2 className="mb-4 text-2xl font-bold">Create Book</h2>

          <div className="-mx-3 mb-6 flex flex-wrap">
            <div className="mb-6 w-full px-3 md:mb-0 md:w-1/2">
              <label
                className="mb-2 block text-xs font-bold uppercase tracking-wide text-gray-700"
                htmlFor="title"
              >
                Title
              </label>
              <input
                className="mb-3 block w-full appearance-none rounded border bg-gray-200 px-4 py-3 leading-tight text-gray-700 focus:bg-white focus:outline-none"
                id="title"
                type="text"
                value={form.title}
                onChange={(e) => setForm({ ...form, title: e.target.value })}
              />
              {errors.title && <p className="text-red-500">{errors.title}</p>}
            </div>
            <div className="w-full px-3 md:w-1/2">
              <label
                className="mb-2 block text-xs font-bold uppercase tracking-wide text-gray-700"
                htmlFor="author"
              >
                Author
              </label>
              <input
                className="block w-full appearance-none rounded border bg-gray-200 px-4 py-3 leading-tight text-gray-700 focus:bg-white focus:outline-none"
                id="author"
                type="text"
                value={form.author}
                onChange={(e) => setForm({ ...form, author: e.target.value })}
              />
              {errors.author && <p className="text-red-500">{errors.author}</p>}
            </div>
          </div>
          <div className="-mx-3 mb-6 flex flex-wrap">
            <div className="mb-6 w-full px-3 md:mb-0 md:w-1/2">
              <label
                className="mb-2 block text-xs font-bold uppercase tracking-wide text-gray-700"
                htmlFor="isbn"
              >
                ISBN
              </label>
              <input
                className="block w-full appearance-none rounded border bg-gray-200 px-4 py-3 leading-tight text-gray-700 focus:bg-white focus:outline-none"
                id="isbn"
                type="text"
                value={form.isbn}
                onChange={(e) => setForm({ ...form, isbn: e.target.value })}
              />
            </div>
            <div className="w-full px-3 md:w-1/2">
              <label
                className="mb-2 block text-xs font-bold uppercase tracking-wide text-gray-700"
                htmlFor="category"
              >
                Category
              </label>
              <select
                className="block w-full appearance-none rounded border bg-gray-200 px-4 py-3 leading-tight text-gray-700 focus:bg-white focus:outline-none"
                id="category"
                value={form.categoryId}
                onChange={(e) =>
                  setForm({ ...form, categoryId: e.target.value })
                }
              >
                {categories.map((category) => (
                  <option key={category.categoryId} value={category.categoryId}>
                    {category.name}
                  </option>
                ))}
              </select>
              {errors.categoryId && (
                <p className="text-red-500">{errors.categoryId}</p>
              )}
            </div>
          </div>

          <div>
            <div className="w-full px-3 md:w-full">
              <label
                className="mb-2 block text-xs font-bold uppercase tracking-wide text-gray-700"
                htmlFor="description"
              >
                Description
              </label>
              <input
                className="block w-full appearance-none rounded border bg-gray-200 px-4 py-3 leading-tight text-gray-700 focus:bg-white focus:outline-none"
                id="description"
                type="text"
                value={form.description}
                onChange={(e) =>
                  setForm({ ...form, description: e.target.value })
                }
              />
            </div>
          </div>

          <label className="mb-2 block" htmlFor="pdfFile">
            PDF File
          </label>
          <input
            className="mb-6 w-full rounded border p-2"
            id="pdfFile"
            type="file"
            onChange={(e) => setForm({ ...form, pdfFile: e.target.files[0] })}
          />
          {errors.pdfFile && <p className="text-red-500">{errors.pdfFile}</p>}
          <label className="mb-2 block" htmlFor="coverFile">
            Cover File
          </label>
          <input
            className="mb-6 w-full rounded border p-2"
            id="coverFile"
            type="file"
            onChange={(e) => setForm({ ...form, coverFile: e.target.files[0] })}
          />

          <button
            type="submit"
            className="mb-4 w-full rounded bg-blue-500 p-2 text-white transition-colors duration-150 ease-linear hover:bg-blue-600"
          >
            Create Book
          </button>
        </form>
      </div>
    </div>
  );
};

export default CreateBook;
