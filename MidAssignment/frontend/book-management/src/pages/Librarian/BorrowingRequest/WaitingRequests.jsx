import React, { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import { useAuthContext } from "../../../context/AuthContext";
import {
  getWaitingRequest,
  updateRequestStatus,
} from "../../../services/requests.service";

const WaitingRequests = () => {
  const [requests, setRequests] = useState([]);
  const { user } = useAuthContext();
  const [pageNumber, setPageNumber] = useState(1);
  const [pageSize, setPageSize] = useState(5);

  const fetchRequests = async () => {
    try {
      const response = await getWaitingRequest(pageNumber, pageSize);
      setRequests(response?.data?.items);
    } catch (error) {
      console.error("Error fetching requests:", error);
    }
  };

  const handleAccept = async (requestId) => {
    try {
      await updateRequestStatus(requestId, JSON.stringify({ status: 1 }));
      alert("Successfully accepted the request");
      fetchRequests();
    } catch (error) {
      console.error("Error accepting request:", error);
    }
  };

  const handleReject = async (requestId) => {
    try {
      //{status:2}
      await updateRequestStatus(requestId, 2);
      alert("Successfully rejected the request");

      fetchRequests();
    } catch (error) {
      console.error("Error rejecting request:", error);
    }
  };
  useEffect(() => {
    fetchRequests();
  }, [pageNumber, pageSize]);

  return (
    <div className="flex flex-col">
      <div className="-my-2 overflow-x-auto sm:-mx-6 lg:-mx-8">
        <div className="inline-block min-w-full py-2 align-middle sm:px-6 lg:px-8">
          <div className="flex justify-end pb-4">
            <form>
              <input
                type="number"
                min="1"
                className="w-full rounded-md border border-gray-300 px-3 py-2 placeholder-gray-300 focus:border-indigo-300 focus:outline-none focus:ring focus:ring-indigo-100 dark:border-gray-600 dark:bg-gray-700 dark:text-white dark:placeholder-gray-500 dark:focus:border-gray-500 dark:focus:ring-gray-900"
              />
              <button type="submit" style={{ display: "none" }} />
            </form>
            <button className="rounded bg-blue-500 px-4 py-2 font-bold text-white hover:bg-blue-700"></button>
          </div>
          <div className="overflow-hidden border-b border-gray-200 shadow sm:rounded-lg">
            <table className="min-w-full divide-y divide-gray-200">
              <thead className="bg-gray-50">
                <div>
                  <tr>
                    <th
                      scope="col"
                      className="px-6 py-3 text-left text-xs font-medium uppercase tracking-wider text-gray-500"
                    >
                      Id
                    </th>
                    <th
                      scope="col"
                      className="px-6 py-3 text-left text-xs font-medium uppercase tracking-wider text-gray-500"
                    >
                      Requestor
                    </th>
                    <th
                      scope="col"
                      className="px-6 py-3 text-left text-xs font-medium uppercase tracking-wider text-gray-500"
                    >
                      Age
                    </th>
                    <th
                      scope="col"
                      className="px-6 py-3 text-left text-xs font-medium uppercase tracking-wider text-gray-500"
                    >
                      Date Requested
                    </th>
                    <th
                      scope="col"
                      className="px-6 py-3 text-left text-xs font-medium uppercase tracking-wider text-gray-500"
                    >
                      Action
                    </th>
                  </tr>
                </div>
              </thead>
              <tbody className="divide-y divide-gray-200 bg-[#F3F4F6]">
                {requests.map((request, index) => (
                  <div key={request.requestId}>
                    <tr>
                      <td className="whitespace-nowrap px-8 py-4 text-sm text-gray-500">
                        {index + 1}
                      </td>
                      <td className="whitespace-nowrap px-7 py-4 text-sm text-gray-500">
                        {request?.user?.lastName}
                      </td>
                      <td className="whitespace-nowrap px-12 py-4 text-sm text-gray-500">
                        {request?.user?.age}
                      </td>
                      <td className="whitespace-nowrap px-6 py-4 text-sm text-gray-500">
                        {new Date(request?.dateRequested).toLocaleDateString(
                          "en-GB",
                          {
                            day: "2-digit",
                            month: "2-digit",
                            year: "2-digit",
                          },
                        )}
                      </td>
                      <td className="whitespace-nowrap px-14 py-6 text-sm text-gray-500">
                        <Link
                          to={`/requests/${request?.requestId}`}
                          className="mr-2 rounded bg-blue-500 px-2 py-1 font-bold text-white hover:bg-blue-700"
                        >
                          View
                        </Link>
                        <button
                          className="mr-2 rounded bg-blue-500 px-2 py-1 font-bold text-white hover:bg-blue-700"
                          onClick={() => handleAccept(request.requestId)}
                        >
                          Accept
                        </button>
                        <button
                          className="mr-2 rounded bg-red-500 px-2 py-1 font-bold text-white hover:bg-red-700"
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
    </div>
  );
};

export default WaitingRequests;
