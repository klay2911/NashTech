import React, { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { getBook, updateBook } from "../../services/books.service";
import { getCategories } from "../../services/categories.service";

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
        //handle the pdfFile and coverFile based on server's response
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
    <form onSubmit={handleSubmit} className="flex flex-col">
      <div>
        <label htmlFor="title" className="mb-2">
          Book Title :{" "}
        </label>
        <input
          type="text"
          id="title"
          value={title}
          onChange={(e) => setTitle(e.target.value)}
          className="mb-4 rounded border border-gray-300 p-2"
        />
        <label htmlFor="title" className="mb-2">
          Author :{" "}
        </label>
        <input
          type="text"
          id="author"
          value={author}
          onChange={(e) => setAuthor(e.target.value)}
          className="mb-4 rounded border border-gray-300 p-2"
        />
        <label htmlFor="title" className="mb-2">
          ISBN :{" "}
        </label>
        <input
          type="text"
          id="isbn"
          value={isbn}
          onChange={(e) => setIsbn(e.target.value)}
          className="mb-4 rounded border border-gray-300 p-2"
        />
        <label htmlFor="title" className="mb-2">
          Description :{" "}
        </label>
        <input
          type="text"
          id="description"
          value={description}
          onChange={(e) => setDescription(e.target.value)}
          className="mb-4 rounded border border-gray-300 p-2"
        />
        <label htmlFor="category" className="mb-2">
          Category :{" "}
        </label>
        <select
          id="category"
          value={categoryId}
          onChange={(e) => setCategoryId(e.target.value)}
          className="mb-4 rounded border border-gray-300 p-2"
        >
          {categories.map((category) => (
            <option key={category.categoryId} value={category.categoryId}>
              {category.name}
            </option>
          ))}
        </select>
        <label htmlFor="pdfFile" className="mb-2">
          PDF File :{" "}
        </label>
        <input
          type="file"
          id="pdfFile"
          onChange={(e) => setPdfFile(e.target.files[0])}
          className="mb-4 rounded border border-gray-300 p-2"
        />

        <label htmlFor="coverFile" className="mb-2">
          Cover File :{" "}
        </label>
        <input
          type="file"
          id="coverFile"
          onChange={(e) => setCoverFile(e.target.files[0])}
          className="mb-4 rounded border border-gray-300 p-2"
        />
      </div>
      <button type="submit" className="rounded bg-blue-500 p-2 text-white">
        Update Book
      </button>
    </form>
  );
};

export default UpdateBook;
