import React, { useState, useEffect } from 'react';
import { getBookUserRequest } from '../../services/requests.service';
import { useAuthContext } from '../../context/AuthContext';
import { EyeFill } from 'react-bootstrap-icons';

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
              <th scope="col" className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                Status
              </th>
              <th scope="col" className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                Action
              </th>
            </tr>
          </thead>
          <tbody className="bg-white divide-y divide-gray-200">
          {requests.map((request, index) => (
              <tr key={request?.id}>
                <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                  {index + 1}
                </td>
                <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                  {request?.title}
                </td>
                <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                  {request?.author}
                </td>
                <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                  {request?.request?.status === 0 ? 'Waiting' : 'Approved'}
                </td>
                <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
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