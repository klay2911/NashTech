import React, { useEffect, useState } from "react";
import { Link, useParams } from "react-router-dom";
import { getBook } from "../../services/books.service";

const DetailsBook = ({}) => {
  const [book, setBook] = useState(null);
  const { id } = useParams();

  useEffect(() => {
    const fetchBook = async () => {
      try {
        const response = await getBook(id);
        setBook(response?.data);
      } catch (error) {
        console.error("Error fetching book:", error);
      }
    };

    fetchBook();
  }, [id]);

  return (
    <div className="flex flex-col">
      <div className="mb-4 flex">
        <div className="w-1/2">
          <img
            src={
              book?.coverPah
                ? `${process.env.REACT_APP_API_BASE_URL}${book?.coverPah}`
                : "https://localhost:44352/covers/Cover.png"
            }
            alt={book?.title}
            className="mb-4 h-40 w-full object-cover"
          />
        </div>
        <div className="w-1/2 pl-4">
          <h2 className="mb-2 text-xl font-bold">{book?.title}</h2>
          <p className="mb-2">
            <strong>Author:</strong> {book?.author}
          </p>
          <p className="mb-2">
            <strong>ISBN:</strong> {book?.isbn}
          </p>
          <p className="mb-2">
            <strong>Description:</strong> {book?.description}
          </p>
          {/* <p className="mb-2"><strong>Category:</strong> {book?.category?.name}</p> */}
        </div>
      </div>
      <div className="mt-10 flex justify-center">
        <Link
          to="/borrowingbooks"
          className="rounded bg-gray-500 px-4 py-2 font-bold text-white hover:bg-gray-600"
        >
          Back
        </Link>
      </div>
    </div>
  );
};

export default DetailsBook;
