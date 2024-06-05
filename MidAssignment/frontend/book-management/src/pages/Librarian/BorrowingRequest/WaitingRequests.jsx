import React, { useEffect, useState } from 'react';
import { getWaitingRequest, updateRequestStatus } from '../../../services/requests.service';
import { Link } from 'react-router-dom';
import { useAuthContext } from '../../../context/AuthContext';



const WaitingRequests = () => {
    const [requests, setRequests] = useState([]);
    const { user } = useAuthContext();
    const userId = user?.NameIdentifier;
    const name = user?.name;

    const [pageNumber, setPageNumber] = useState(1);
    const [pageSize, setPageSize] = useState(5);
    const [hasNextPage, setHasNextPage] = useState(false);
    const [hasPreviousPage, setHasPreviousPage] = useState(false);

    const fetchRequests = async () => {
        try {
            const response = await getWaitingRequest(pageNumber, pageSize);
            setRequests(response?.data?.items);
            setHasNextPage(response?.data?.hasNextPage);
            setHasPreviousPage(response?.data?.hasPreviousPage);
        } catch (error) {
            console.error("Error fetching requests:", error);
        }
    }

    const handleAccept = async (requestId) => {
      try {
        await updateRequestStatus(requestId, { status: 1, modifyBy: name, modifyAt: Date.now()});
        alert('Successfully accepted the request');
        fetchRequests();
      } catch (error) {
        console.error("Error accepting request:", error);
      }
    };
    
    const handleReject = async (requestId) => {
      try {
        await updateRequestStatus(requestId, { requestStatus: 2,  });
        alert('Successfully rejected the request');

        fetchRequests();
      } catch (error) {
        console.error("Error rejecting request:", error);
      }
    };
    useEffect(() => {
      fetchRequests();
    }, [pageNumber, pageSize, handleAccept, handleReject]);


    return (
        <div className="flex flex-col">
          <div className="-my-2 overflow-x-auto sm:-mx-6 lg:-mx-8">
            <div className="py-2 align-middle inline-block min-w-full sm:px-6 lg:px-8">
              <div className="flex justify-end pb-4">
                <form >
                    <input 
                    type="number" 
                    min="1"
                    className="w-full px-3 py-2 placeholder-gray-300 border border-gray-300 rounded-md focus:outline-none focus:ring focus:ring-indigo-100 focus:border-indigo-300 dark:bg-gray-700 dark:text-white dark:placeholder-gray-500 dark:border-gray-600 dark:focus:ring-gray-900 dark:focus:border-gray-500"
                    />
                    <button type="submit" style={{ display: 'none' }} />
                </form>
                <button className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded">
                </button>
              </div>
              <div className="shadow overflow-hidden border-b border-gray-200 sm:rounded-lg">
                <table className="min-w-full divide-y divide-gray-200">
                  <thead className="bg-gray-50">
                    <div>
                      <tr>
                        <th scope="col" className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                          Id
                        </th>
                        <th scope="col" className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                          Requestor
                        </th>
                        <th scope="col" className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                          Age
                        </th>
                        <th scope="col" className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                          Date Requested
                        </th>
                        <th scope="col" className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                          Action
                        </th>
                      </tr>
                    </div>
                  </thead>
                  <tbody className="bg-[#F3F4F6] divide-y divide-gray-200">
                    {requests.map((request, index) => (
                      <div key={request.requestId}>
                        <tr>
                          <td className="px-8 py-4 whitespace-nowrap text-sm text-gray-500">
                            {index + 1}
                          </td>
                          <td className="px-7 py-4 whitespace-nowrap text-sm text-gray-500">
                              {request?.user?.lastName}
                          </td>
                          <td className="px-12 py-4 whitespace-nowrap text-sm text-gray-500">
                              {request?.user?.age}
                          </td>
                          <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                          {new Date(request?.dateRequested).toLocaleDateString('en-GB', {
                              day: '2-digit',
                              month: '2-digit',
                              year: '2-digit'
                          })}
                          </td>
                          <td className="px-14 py-6 whitespace-nowrap text-sm text-gray-500">
                          <Link 
                            to={`/requests/${request?.requestId}`}
                            className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-1 px-2 rounded mr-2"
                          >
                            View
                          </Link>
                              <button className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-1 px-2 rounded mr-2"
                                      onClick={() => handleAccept(request.requestId)}
                              >
                                  Accept
                              </button>
                              <button className="bg-red-500 hover:bg-red-700 text-white font-bold py-1 px-2 rounded mr-2"
                                      onClick={() => handleReject(request.requestId)}
                              >
                                  Reject
                              </button>
                          </td>
                        </tr>
                      </div>
                    ))} 
                  </tbody>
                </table>
              </div>
            </div>
        </div>
        {/* <div className="flex">
            <div className="w-1/2 p-2">
                <button 
                onClick={handlePrevious} 
                disabled={!hasPreviousPage}
                className={`w-full bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded ${!hasPreviousPage ? 'opacity-50 cursor-not-allowed' : ''}`}
                >
                Previous
                </button>
            </div>
            <div className="w-1/2 p-2">
                <button 
                onClick={handleNext} 
                disabled={!hasNextPage}
                className={`w-full bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded ${!hasNextPage ? 'opacity-50 cursor-not-allowed' : ''}`}
                >
                Next
                </button>
            </div>
        </div> */}
        </div>
      );
}

export default WaitingRequests;