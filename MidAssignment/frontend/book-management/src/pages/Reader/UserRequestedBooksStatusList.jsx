import React, { useEffect, useState } from "react";
import { EyeFill } from "react-bootstrap-icons";
import { useAuthContext } from "../../context/AuthContext";
import { getBookUserRequest } from "../../services/requests.service";

const UserRequestedBookStatusList = ({}) => {
  const { user } = useAuthContext();
  const userId = user?.NameIdentifier;
  const [requests, setRequests] = useState([]);

  useEffect(() => {
    fetchUserRequests();
  }, [userId]);

  const fetchUserRequests = async () => {
    try {
      const response = await getBookUserRequest(userId);
      setRequests(response?.data?.items);
    } catch (error) {
      console.error("Error fetching user requests:", error);
    }
  };

  return (
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
                    <td className="whitespace-nowrap px-6 py-4 text-sm text-gray-500">
                      {request?.request?.status === 0 ? "Waiting" : "Approved"}
                    </td>
                    <td className="whitespace-nowrap px-6 py-4 text-sm text-gray-500">
                      <button
                        className="text-blue-500 hover:text-blue-700"
                        disabled={new Date() > new Date(request?.expiredDate)}
                      >
                        <EyeFill className="mr-2" />
                      </button>
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        </div>
      </div>
    </div>
  );
};

export default UserRequestedBookStatusList;
