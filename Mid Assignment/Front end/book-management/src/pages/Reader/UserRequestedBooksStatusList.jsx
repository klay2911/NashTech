import React, { useEffect, useState } from "react";
import { EyeFill } from "react-bootstrap-icons";
import { Document, Page } from "react-pdf";
import { Pagination, SearchBar } from "../../components";
import { useAuthContext } from "../../context/AuthContext";
import { getBookUserRequest } from "../../services/requests.service";

const UserRequestedBookStatusList = ({}) => {
  const { user } = useAuthContext();
  const [pageNumber, setPageNumber] = useState(1);
  const [pageSize, setPageSize] = useState(5);
  const [hasNextPage, setHasNextPage] = useState(false);
  const [hasPreviousPage, setHasPreviousPage] = useState(false);
  const [searchTerm, setSearchTerm] = useState("");
  const userId = user?.NameIdentifier;
  const [requests, setRequests] = useState([]);
  const [totalCount, setTotalCount] = useState(0);

  useEffect(() => {
    fetchUserRequests();
  }, [pageNumber, pageSize, searchTerm]);

  const fetchUserRequests = async () => {
    if (!userId) {
      return;
    }
    try {
      const response = await getBookUserRequest(
        userId,
        pageNumber,
        pageSize,
        searchTerm,
      );
      setRequests(response?.data?.items);
      setHasNextPage(response?.data?.hasNextPage);
      setHasPreviousPage(response?.data?.hasPreviousPage);
      setTotalCount(response?.data?.totalCount);
    } catch (error) {
      console.error("Error fetching user requests:", error);
    }
  };

  const handlePageSizeChange = (event) => {
    setPageSize(Number(event.target.value));
  };

  return (
    <div className="flex flex-col">
      <SearchBar onSearch={setSearchTerm} />
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
                  <th
                    scope="col"
                    className="px-6 py-3 text-left text-xs font-medium uppercase tracking-wider text-gray-500"
                  >
                    Status
                  </th>
                  <th
                    scope="col"
                    className="px-6 py-3 text-left text-xs font-medium uppercase tracking-wider text-gray-500"
                  >
                    Action
                  </th>
                </tr>
              </thead>
              <tbody className="divide-y divide-gray-200 bg-white">
                {requests.map((request, index) => (
                  <tr key={request?.id}>
                    <td className="whitespace-nowrap px-6 py-4 text-sm text-gray-500">
                      {index + 1}
                    </td>
                    <td className="whitespace-nowrap px-6 py-4 text-sm text-gray-500">
                      {request?.title}
                    </td>
                    <td className="whitespace-nowrap px-6 py-4 text-sm text-gray-500">
                      {request?.author}
                    </td>
                    <td className="whitespace-nowrap px-6 py-4 text-sm">
                      <span
                        style={{
                          color:
                            request?.request?.status === 0
                              ? "darkorange"
                              : request?.request?.status === 1
                                ? "green"
                                : request?.request?.status === 2
                                  ? "red"
                                  : "gray",
                        }}
                      >
                        {{
                          0: "Waiting",
                          1: "Approved",
                          2: "Rejected",
                        }[request?.request?.status] || "Unknown status"}
                      </span>
                    </td>
                    <td className="whitespace-nowrap px-6 py-4 text-sm text-gray-500">
                      <button
                        className="text-blue-500 hover:text-blue-700"
                        disabled={new Date() > new Date(request?.expiredDate)}
                      >
                        <EyeFill className="mr-2" />
                      </button>
                      <Document
                        file={`${process.env.REACT_APP_API_BASE_URL}${request?.bookPath}`}
                      >
                        <Page pageNumber={1} />
                      </Document>
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
        fetchData={fetchUserRequests}
      />
    </div>
  );
};

export default UserRequestedBookStatusList;
