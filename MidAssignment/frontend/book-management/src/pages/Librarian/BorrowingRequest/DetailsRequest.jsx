import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { getBook } from "../../../services/books.service";
import { getRequest } from "../../../services/requests.service";

const DetailsRequest = ({}) => {
  const [request, setRequest] = useState(null);
  const [books, setBooks] = useState([]);
  const { requestId } = useParams();

  useEffect(() => {
    const fetchData = async () => {
      try {
        const requestResponse = await getRequest(requestId);
        setRequest(requestResponse.data);

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
  }, [requestId]);

  return (
    <div>
      <div>
        <button className="mr-2 rounded bg-blue-500 px-2 py-1 font-bold text-white hover:bg-blue-700">
          Accept
        </button>
        <button className="mr-2 rounded bg-red-500 px-2 py-1 font-bold text-white hover:bg-red-700">
          Reject
        </button>
      </div>
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
    </div>
  );
};

export default DetailsRequest;
