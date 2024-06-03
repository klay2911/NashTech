import React, { useState } from 'react';

const BorrowingBooks = () => {
    const [bookIds, setBookIds] = useState([]);

    const handleAddToRequest = (bookId) => {
        const updatedBookIds = [...bookIds, bookId];
        setBookIds(updatedBookIds);
        sessionStorage.setItem('bookIds', JSON.stringify(updatedBookIds));
    };

    const handleRemoveFromRequest = (bookId) => {
        const updatedBookIds = bookIds.filter((id) => id !== bookId);
        setBookIds(updatedBookIds);
        sessionStorage.setItem('bookIds', JSON.stringify(updatedBookIds));
    };

    const books = [
        {
            id: 1,
            cover: '/path/to/book1.jpg',
            title: 'Book 1',
            author: 'Author 1',
        },
        {
            id: 2,
            cover: '/path/to/book2.jpg',
            title: 'Book 2',
            author: 'Author 2',
        },
    ];

    return (
        <div className="grid grid-cols-5 gap-4">
            {books.map((book) => (
                <div key={book.id} className="bg-white p-4 rounded shadow">
                    <img src={book.cover} alt={book.title} className="w-full h-40 object-cover mb-4" />
                    <h3 className="text-xl font-bold mb-2">{book.title}</h3>
                    <p className="text-gray-500 mb-4">{book.author}</p>
                    <div className="flex justify-between">
                        {bookIds.includes(book.id) ? (
                            <button
                                className="bg-red-500 hover:bg-red-600 text-white font-bold py-2 px-4 rounded"
                                onClick={() => handleRemoveFromRequest(book.id)}
                            >
                                Remove from Request
                            </button>
                        ) : (
                            <button
                                className="bg-blue-500 hover:bg-blue-600 text-white font-bold py-2 px-4 rounded"
                                onClick={() => handleAddToRequest(book.id)}
                            >
                                Add to Request
                            </button>
                        )}
                        <button className="bg-gray-500 hover:bg-gray-600 text-white font-bold py-2 px-4 rounded">
                            View Detail
                        </button>
                    </div>
                </div>
            ))}
        </div>
    );
};

export default BorrowingBooks;
