import React, { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import { Pagination, SearchBar } from "../../../components";
import {
  getWaitingRequest,
  updateRequestStatus,
} from "../../../services/requests.service";

const WaitingRequests = () => {
  const [requests, setRequests] = useState([]);
  const [pageNumber, setPageNumber] = useState(1);
  const [pageSize, setPageSize] = useState(5);
  const [hasNextPage, setHasNextPage] = useState(false);
  const [hasPreviousPage, setHasPreviousPage] = useState(false);
  const [searchTerm, setSearchTerm] = useState("");
  const [totalCount, setTotalCount] = useState(0);

  const fetchRequests = async () => {
    try {
      const response = await getWaitingRequest(
        pageNumber,
        pageSize,
        searchTerm,
      );
      setRequests(response?.data?.items);
      setHasNextPage(response?.data?.hasNextPage);
      setHasPreviousPage(response?.data?.hasPreviousPage);
      setTotalCount(response?.data?.totalCount);
    } catch (error) {
      console.error("Error fetching requests:", error);
    }
  };

  const handlePageSizeChange = (event) => {
    setPageSize(Number(event.target.value));
  };

  const handleAccept = async (requestId) => {
    try {
      await updateRequestStatus(requestId, 1);
      alert("Successfully accepted the request");
      fetchRequests();
    } catch (error) {
      console.error("Error accepting request:", error);
    }
  };

  const handleReject = async (requestId) => {
    try {
      await updateRequestStatus(requestId, 2);
      alert("Successfully rejected the request");
      fetchRequests();
    } catch (error) {
      console.error("Error rejecting request:", error);
    }
  };

  useEffect(() => {
    fetchRequests();
  }, [pageNumber, pageSize, searchTerm]);

  return (
    <div className="flex flex-col">
      <SearchBar onSearch={setSearchTerm} />
      <div className="-my-2 overflow-x-auto sm:-mx-6 lg:-mx-8">
        <div className="inline-block min-w-full py-2 align-middle sm:px-6 lg:px-8">
          <div className="overflow-hidden border-b border-gray-200 shadow sm:rounded-lg">
            <table className="min-w-full divide-y divide-gray-200">
              <thead className="bg-gray-50">
                <tr>
                  <th className="px-6 py-3 text-left text-xs font-medium uppercase tracking-wider text-gray-500">
                    Id
                  </th>
                  <th className="px-6 py-3 text-left text-xs font-medium uppercase tracking-wider text-gray-500">
                    Requestor
                  </th>
                  <th className="px-8 py-3 text-left text-xs font-medium uppercase tracking-wider text-gray-500">
                    Age
                  </th>
                  <th className="px-10 py-3 text-left text-xs font-medium uppercase tracking-wider text-gray-500">
                    Date Requested
                  </th>
                  <th className="px-10 py-3 text-center text-xs font-medium uppercase tracking-wider text-gray-500">
                    STATUS
                  </th>
                  <th className="px-10 py-3 text-center text-xs font-medium uppercase tracking-wider text-gray-500">
                    APPROVER
                  </th>
                  <th className="px-10 py-3 text-center text-xs font-medium uppercase tracking-wider text-gray-500">
                    Action
                  </th>
                </tr>
              </thead>
              <tbody className="divide-y divide-gray-200 bg-[#F3F4F6]">
                {requests.map((request, index) => (
                  <tr key={request.requestId}>
                    <td className="whitespace-nowrap px-6 py-4 text-sm text-gray-500">
                      {index + 1}
                    </td>
                    <td className="whitespace-nowrap px-6 py-4 text-sm text-gray-500">
                      {request?.user?.lastName}
                    </td>
                    <td className="whitespace-nowrap px-6 py-4 text-sm text-gray-500">
                      {request?.user?.age}
                    </td>
                    <td className="whitespace-nowrap px-6 py-4 text-sm text-gray-500">
                      {request?.dateRequested}
                    </td>
                    <td className="whitespace-nowrap px-6 py-4 text-center text-sm text-gray-500">
                      <span
                        style={{
                          color:
                            request?.status === 0
                              ? "darkorange"
                              : request?.status === 1
                                ? "green"
                                : request?.status === 2
                                  ? "red"
                                  : "gray",
                        }}
                      >
                        {{ 0: "Waiting", 1: "Approved", 2: "Rejected" }[
                          request?.status
                        ] || "Unknown status"}
                      </span>
                    </td>
                    <td className="whitespace-nowrap px-6 py-4 text-center text-sm text-gray-500">
                      {request?.approver?.lastName}
                    </td>
                    <td className="whitespace-nowrap px-6 py-4 text-sm text-gray-500">
                      <Link
                        to={`/requests/${request?.requestId}`}
                        className="mr-2 rounded bg-blue-500 px-2 py-1 font-bold text-white hover:bg-blue-700"
                      >
                        View
                      </Link>
                      <button
                        className={`mr-2 rounded px-2 py-1 font-bold text-white ${request?.status === 1 || request?.status === 2 ? "bg-gray-500 hover:bg-gray-500" : "bg-blue-500 hover:bg-blue-700"}`}
                        onClick={() => handleAccept(request.requestId)}
                        disabled={
                          request?.status === 1 || request?.status === 2
                        }
                      >
                        Accept
                      </button>
                      <button
                        className={`mr-2 rounded px-2 py-1 font-bold text-white ${request?.status === 1 || request?.status === 2 ? "bg-gray-500 hover:bg-gray-500" : "bg-red-500 hover:bg-red-700"}`}
                        onClick={() => handleReject(request.requestId)}
                        disabled={
                          request?.status === 1 || request?.status === 2
                        }
                      >
                        Reject
                      </button>
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        </div>
      </div>
      <Pagination
        pageNumber={pageNumber}
        hasNextPage={hasNextPage}
        hasPreviousPage={hasPreviousPage}
        setPageNumber={setPageNumber}
        pageSize={pageSize}
        handlePageSizeChange={handlePageSizeChange}
        totalItems={totalCount}
        fetchData={fetchRequests}
      />
    </div>
  );
};

export default WaitingRequests;
