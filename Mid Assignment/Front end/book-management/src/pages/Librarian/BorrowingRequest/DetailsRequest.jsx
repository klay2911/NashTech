import React, { useEffect, useState } from "react";
import { Link, useNavigate, useParams } from "react-router-dom";
import { getBook } from "../../../services/books.service";
import {
  getRequest,
  updateRequestStatus,
} from "../../../services/requests.service";

const DetailsRequest = ({}) => {
  const [books, setBooks] = useState([]);
  const { requestId } = useParams();
  const navigate = useNavigate();
  const [requestStatus, setRequestStatus] = useState(null);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const requestResponse = await getRequest(requestId);
        setRequestStatus(requestResponse.data.status);
        const bookPromises = requestResponse.data.bookIds.map((bookId) =>
          getBook(bookId),
        );
        const bookResponses = await Promise.all(bookPromises);
        const bookData = bookResponses.map((response) => response.data);
        setBooks(bookData);
      } catch (error) {
        console.error(error);
      }
    };
    fetchData();
  }, []);

  const handleAccept = async () => {
    try {
      setRequestStatus(1);
      await updateRequestStatus(requestId, 1);
      alert("Successfully accepted the request");
      navigate("/requests");
    } catch (error) {
      console.error("Error accepting request:", error);
    }
  };

  const handleReject = async () => {
    try {
      setRequestStatus(2);
      await updateRequestStatus(requestId, 2);
      alert("Successfully rejected the request");
      navigate("/requests");
    } catch (error) {
      console.error("Error rejecting request:", error);
    }
  };

  return (
    <div>
      <div className="flex flex-col">
        <div className="-my-2 overflow-x-auto sm:-mx-6 lg:-mx-8">
          <div className="inline-block min-w-full py-2 align-middle sm:px-6 lg:px-8">
            <div className="overflow-hidden border-b border-gray-200 shadow sm:rounded-lg">
              <table className="min-w-full divide-y divide-gray-200">
                <thead className="bg-gray-50">
                  <tr>
                    <th
                      scope="col"
                      className="px-6 py-3 text-left text-xs font-medium uppercase tracking-wider text-gray-500"
                    >
                      Index
                    </th>
                    <th
                      scope="col"
                      className="px-6 py-3 text-left text-xs font-medium uppercase tracking-wider text-gray-500"
                    >
                      Title
                    </th>
                    <th
                      scope="col"
                      className="px-6 py-3 text-left text-xs font-medium uppercase tracking-wider text-gray-500"
                    >
                      Author
                    </th>
                  </tr>
                </thead>
                <tbody className="divide-y divide-gray-200 bg-white">
                  {books.map((book, index) => (
                    <tr key={book.id}>
                      <td className="whitespace-nowrap px-6 py-4 text-sm text-gray-500">
                        {index + 1}
                      </td>
                      <td className="whitespace-nowrap px-6 py-4 text-sm text-gray-500">
                        {book.title}
                      </td>
                      <td className="whitespace-nowrap px-6 py-4 text-sm text-gray-500">
                        {book.author}
                      </td>
                    </tr>
                  ))}
                </tbody>
              </table>
            </div>
          </div>
        </div>
      </div>
      <div className="mt-10">
        <button
          onClick={handleAccept}
          className={`mr-8 transform rounded px-4 py-2 font-bold text-white transition duration-200 ease-in-out ${requestStatus !== 0 ? "bg-gray-500" : "bg-blue-500 hover:bg-blue-700"}`}
          disabled={requestStatus !== 0}
        >
          Accept
        </button>
        <button
          onClick={handleReject}
          className={`mr-4 transform rounded px-4 py-2 font-bold text-white transition duration-200 ease-in-out ${requestStatus !== 0 ? "bg-gray-500" : "bg-red-500 hover:bg-red-700"}`}
          disabled={requestStatus !== 0}
        >
          Reject
        </button>
      </div>
      <div className="mt-5 flex justify-center">
        <Link
          to="/requests"
          className="w-1/3 transform rounded bg-gray-500 p-2 text-center text-white transition-colors duration-150 ease-linear hover:scale-105 hover:bg-gray-600"
        >
          Back
        </Link>
      </div>
    </div>
  );
};

export default DetailsRequest;
