import React, { useEffect, useState } from 'react';
import { getRequest} from '../../../services/requests.service';
import { getBook } from '../../../services/books.service';
import { useParams } from 'react-router-dom';

const DetailsRequest = ({}) => {
    const [request, setRequest] = useState(null);
    const [books, setBooks] = useState([]);
    const { requestId } = useParams();

    useEffect(() => {
        const fetchData = async () => {
            try {
                const requestResponse = await getRequest(requestId);
                setRequest(requestResponse.data);

                const bookPromises = requestResponse.data.bookIds.map((bookId) => getBook(bookId));
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
            <button className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-1 px-2 rounded mr-2">
                Accept
            </button>
            <button className="bg-red-500 hover:bg-red-700 text-white font-bold py-1 px-2 rounded mr-2">
                Reject
            </button>
            </div>
            <div className="flex flex-col">
                <div className="-my-2 overflow-x-auto sm:-mx-6 lg:-mx-8">
                    <div className="py-2 align-middle inline-block min-w-full sm:px-6 lg:px-8">
                    <div className="shadow overflow-hidden border-b border-gray-200 sm:rounded-lg">
                        <table className="min-w-full divide-y divide-gray-200">
                        <thead className="bg-gray-50">
                            <tr>
                            <th scope="col" className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                                Index
                            </th>
                            <th scope="col" className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                                Title
                            </th>
                            <th scope="col" className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                                Author
                            </th>
                            </tr>
                        </thead>
                        <tbody className="bg-white divide-y divide-gray-200">
                            {books.map((book, index) => (
                            <tr key={book.id}>
                                <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                                {index + 1}
                                </td>
                                <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                                {book.title}
                                </td>
                                <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
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
