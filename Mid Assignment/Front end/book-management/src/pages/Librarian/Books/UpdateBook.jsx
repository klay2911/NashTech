import React, { useEffect, useState } from "react";
import { Link, useNavigate, useParams } from "react-router-dom";
import { getBook, updateBook } from "../../../services/books.service";
import { getCategories } from "../../../services/categories.service";

const UpdateBook = () => {
  const [title, setTitle] = useState("");
  const [author, setAuthor] = useState("");
  const [isbn, setIsbn] = useState("");
  const [description, setDescription] = useState("");
  const [pdfFile, setPdfFile] = useState(null);
  const [coverFile, setCoverFile] = useState(null);
  const [categoryId, setCategoryId] = useState("");
  const [categories, setCategories] = useState([]);
  const navigate = useNavigate();
  const { id } = useParams();

  useEffect(() => {
    getBook(id)
      .then((response) => {
        const book = response?.data;
        setTitle(response?.data?.title);
        setAuthor(book.author);
        setIsbn(book.isbn);
        setDescription(book.description);
        setCategoryId(book.categoryId);
      })
      .catch((error) => {
        console.error("Error fetching book:", error);
      });

    // Fetch the categories
    getCategories()
      .then((response) => {
        setCategories(response?.data?.items);
      })
      .catch((error) => {
        console.error("Error fetching categories:", error);
      });
  }, [id]);

  const handleSubmit = (event) => {
    event.preventDefault();

    const formData = new FormData();
    formData.append("title", title);
    formData.append("author", author);
    formData.append("isbn", isbn);
    formData.append("description", description);
    formData.append("categoryId", categoryId);
    formData.append("coverFile", coverFile);
    formData.append("pdfFile", pdfFile);

    updateBook(id, formData)
      .then((response) => {
        console.log("Updated book:", response?.data);
        navigate("/books");
      })
      .catch((error) => {
        console.error("Error updating book:", error);
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
          <h2 className="mb-4 text-2xl font-bold">Update Book</h2>

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
                value={title}
                onChange={(e) => setTitle(e.target.value)}
              />
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
                value={author}
                onChange={(e) => setAuthor(e.target.value)}
              />
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
                value={isbn}
                onChange={(e) => setIsbn(e.target.value)}
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
                value={categoryId}
                onChange={(e) => setCategoryId(e.target.value)}
              >
                {categories.map((category) => (
                  <option key={category.categoryId} value={category.categoryId}>
                    {category.name}
                  </option>
                ))}
              </select>
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
                value={description}
                onChange={(e) => setDescription(e.target.value)}
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
            onChange={(e) => setPdfFile(e.target.files[0])}
          />

          <label className="mb-2 block" htmlFor="coverFile">
            Cover File
          </label>
          <input
            className="mb-6 w-full rounded border p-2"
            id="coverFile"
            type="file"
            onChange={(e) => setCoverFile(e.target.files[0])}
          />

          <button
            type="submit"
            className="mb-4 w-full rounded bg-blue-500 p-2 text-white transition-colors duration-150 ease-linear hover:bg-blue-600"
          >
            Update Book
          </button>

          <Link
            to="/books"
            className="w-full rounded bg-gray-500 p-2 text-center text-white transition-colors duration-150 ease-linear hover:bg-gray-600"
          >
            Back
          </Link>
        </form>
      </div>
    </div>
  );
};

export default UpdateBook;
