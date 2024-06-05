import React, { useState } from 'react';

const Paginations = () =>{
    const [pageNumber, setPageNumber] = useState(1);
    const [pageSize, setPageSize] = useState(5);
    const [inputValue, setInputValue] = useState(pageSize);
    const [hasNextPage, setHasNextPage] = useState(false);
    const [hasPreviousPage, setHasPreviousPage] = useState(false);
    const handleNext = () => {
        if (hasNextPage) {
          setPageNumber(prevPageNumber => prevPageNumber + 1);
        }
      };
    
      const handlePrevious = () => {
        if (hasPreviousPage && pageNumber > 1) {
          setPageNumber(prevPageNumber => prevPageNumber - 1);
        }
      };
    
      const handlePageSizeChange = (event) => {
        setInputValue(event.target.value);;
      };
      const handleSubmit = (event) => {
        event.preventDefault();
        setPageSize(inputValue);
        setPageNumber(1); 
      };
    return(
        <div style={{ position: 'fixed', right: -50, bottom: 80 }}>
                <form onSubmit={handleSubmit}>
                    <input 
                        type="number" 
                        min="1"
                        value={inputValue} 
                        onChange={handlePageSizeChange} 
                        className="w-1/2 px-3 py-2 placeholder-gray-300 border border-gray-300 rounded-md focus:outline-none focus:ring focus:ring-indigo-100 focus:border-indigo-300 dark:bg-[#F3F4F6]-700 dark:text-[#333333] dark:placeholder-gray-500 dark:border-gray-600 dark:focus:ring-gray-900 dark:focus:border-gray-500"
                    />
                    <button type="submit" style={{ display: 'none' }} />
                </form>
            </div>
    );
}
export default Paginations;